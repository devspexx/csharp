If you don't have BunifuUI Framework you should edit <file>.Designer.cs and remove controls
that are referenced to that framework to prevent further issues. 

UI Preview
https://i.imgur.com/83kyvvN.png
https://i.imgur.com/762fLOI.png
https://i.imgur.com/GpfCFnz.png
https://i.imgur.com/01yhWxW.png
https://i.imgur.com/CnicCUJ.png
https://i.imgur.com/zbTSqJI.png
https://i.imgur.com/5Mm5hIP.png
https://imgur.com/a/fGRfcvK.png
https://i.imgur.com/iMNMTvT.png
https://i.imgur.com/u4f0XEr.png
https://i.imgur.com/JySsbNZ.png

# How do I use it?
Reference it in your MAIN FORM like so:

public partial class App : ModernForm
instead of 
public partial class App : Form

IMPORTANT! Add this after InitializeComponent() method
if (DesignMode) return;
Prevents potentional onPaint events to be fired