#include "LittleFS.h"
#include "Debugger.h"

#define FORMAT_LITTLEFS_IF_FAILED true

namespace DataSaving
{
    bool Initialize()
    {
        bool suc = LittleFS.begin(FORMAT_LITTLEFS_IF_FAILED);
        if(!suc) DebugError("Failed to initialize LittleFS");
        return suc;
    }

    
    bool FileExists(const char* path)
    {
        return LittleFS.exists(path);
    }

    bool CreateFile(const char* path)
    {
        if(FileExists(path)) return false;

        LittleFS.open(path, "w").close();

        return true;
    }

    size_t WriteData(const char* path, const uint8_t* data, const size_t len, bool createFileIfDoesntExist = true)
    {
        if(!FileExists(path))
        {
            if(!createFileIfDoesntExist || !CreateFile(path)) return -1;
        }

        File file = LittleFS.open(path, "w");
        if(!file)
        {
            DebugError("Failed to open file for writing");
            return -1;
        }

        size_t res = file.write(data, len);

        file.close();

        return res;

    }

    size_t ReadData(const char* path, uint8_t* data, const size_t len)
    { 
        if(!FileExists(path)) return false;

        File file = LittleFS.open(path, "r");
        if(!file)
        {
            DebugError("Failed to open file for reading");
            return -1;
        }

        size_t res = file.read(data, len);

        file.close();

        return res;
    }
}