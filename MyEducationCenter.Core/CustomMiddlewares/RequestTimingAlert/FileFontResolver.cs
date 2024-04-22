using PdfSharp.Fonts;


namespace MyEducationCenter.Core;

public class FileFontResolver : IFontResolver
{
    public string DefaultFontName => throw new NotImplementedException();

    public byte[] GetFont(string faceName)
    {
        using (var ms = new MemoryStream())
        {
            using (var fs = File.Open(faceName, FileMode.Open))
            {
                fs.CopyTo(ms);
                ms.Position = 0;
                return ms.ToArray();
            }
        }
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (familyName.Equals("Verdana", StringComparison.CurrentCultureIgnoreCase))
        {
            if (isBold && isItalic)
            {
                return new FontResolverInfo("Fonts/verdana-bold.ttf");
            }
            else if (isBold)
            {
                return new FontResolverInfo("Fonts/verdana-bold.ttf");
            }
            else if (isItalic)
            {
                return new FontResolverInfo("Fonts/verdana-bold.ttf");
            }
            else
            {
                return new FontResolverInfo("Fonts/verdana-bold.ttf");
            }
        }
        return null;
    }
}
