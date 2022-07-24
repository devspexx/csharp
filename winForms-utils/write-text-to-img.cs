private Image DrawText(string text, Font font, Color textColor, Color backColor, Size backgroundControlRendererSize)
{
    //first, create a dummy bitmap just to get a graphics object
    Image img = new Bitmap(1, 1);
    Graphics drawing = Graphics.FromImage(img);

    //measure the string to see how big the image needs to be
    SizeF textSize = drawing.MeasureString(text, font);

    //free up the dummy image and old graphics object
    img.Dispose();
    drawing.Dispose();

    //create a new image of the right size
    img = new Bitmap(backgroundControlRendererSize.Width, backgroundControlRendererSize.Height);

    drawing = Graphics.FromImage(img);

    //paint the background
    drawing.Clear(backColor);

    //create a brush for the text
    Brush textBrush = new SolidBrush(textColor);

    drawing.DrawString(text, font, textBrush, 0, backgroundControlRendererSize.Height - textSize.Height);

    drawing.Save();
    textBrush.Dispose();
    drawing.Dispose();

    return img;
}
