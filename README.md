#How To Install

Add 

- for newest update
```csharp
"com.snorlax.locale": "https://github.com/snorluxe/locale.git?path=Assets/_Root",
```

- for excactly version
```csharp
"com.snorlax.locale": "https://github.com/snorluxe/locale.git?path=Assets/_Root#1.0.2",
```

To `Packages/manifest.json`


#Usage

- Getting full language ID (e.g. “en_es”):
```c#
using Snorlax.Locale;

PreciseLocale.GetLanguageID();
```

- Getting language code (e.g. “es”):
```c#
using Snorlax.Locale;

PreciseLocale.GetLanguage();
```

- Getting region code (e.g. “US”):
```c#
using Snorlax.Locale;

PreciseLocale.GetRegion();
```

- Getting currency code (e.g. “USD”):
```c#
using Snorlax.Locale;

PreciseLocale.GetCurrencyCode();
```

- Getting currency symbol (e.g. “$”):
```c#
using Snorlax.Locale;

PreciseLocale.GetCurrencySymbol();
```

#Note

- plugin takes language preferences from device settings, not actual user location.

![image](https://user-images.githubusercontent.com/44673303/154239313-b94153d1-368e-40fd-a009-d6c81986ced8.png)
![image](https://user-images.githubusercontent.com/44673303/154239827-33bf0d61-44ab-4e7d-921b-cfa629dec083.png)


-You might notice that on Android languageID = language + “_” + region, which isn’t true on iOS!