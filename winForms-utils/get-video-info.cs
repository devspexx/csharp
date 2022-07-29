// nuget pkg mgr Install-Package NReco.VideoInfo -Version 1.1.1
// https://www.nuget.org/packages/NReco.VideoInfo/

NReco.VideoInfo.FFProbe ffProbe = new NReco.VideoInfo.FFProbe();
NReco.VideoInfo.MediaInfo videoInfo = ffProbe.GetMediaInfo(file);
NReco.VideoInfo.MediaInfo.StreamInfo streamInfo = videoInfo.Streams[0];

string dimensions = streamInfo.Width + " x " + streamInfo.Height;
string fps = streamInfo.FrameRate.ToString();
string pixel_format = streamInfo.PixelFormat;
string codec_longname = streamInfo.CodecLongName;
