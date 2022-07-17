string url = "http://stream1.radiocity.si:8000/CityMp3128.mp3"; // eg. radio city SI

MediaFoundationReader mediaFoundationReader = new MediaFoundationReader(url);
wasapiOut = new WasapiOut();

wasapiOut.Init(mediaFoundationReader);
wasapiOut.Play();