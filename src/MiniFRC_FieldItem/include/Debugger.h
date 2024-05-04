#pragma once

#define DEBUGINFO 1
#define DEBUGWARNING 1
#define DEBUGERROR 1


#if DEBUGINFO == 1
#define DebugInfo(x) Serial.println("[I] " + String(x))
#define DebugInfoF(x, ...) Serial.printf(("[I] " + String(x)).c_str(), __VA_ARGS__)
#else
#define DebugInfo(x)
#define DebugInfoF(x, ...)
#endif

#if DEBUGWARNING == 1
#define DebugWarning(x) Serial.println("[W] " + String(x))
#define DebugWarningF(x, ...) Serial.printf(("[W] " + String(x)).c_str(), __VA_ARGS__)
#else
#define DebugWarning(x)
#define DebugWarningF(x, ...)
#endif


#if DEBUGERROR == 1
#define DebugError(x) Serial.println("[E] " + String(x))
#define DebugErrorF(x, ...) Serial.printf(("[E] " + String(x)).c_str(), __VA_ARGS__)
#else
#define DebugError(x)
#define DebugErrorF(x, ...)
#endif