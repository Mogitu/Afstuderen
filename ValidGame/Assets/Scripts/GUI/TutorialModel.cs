/// <summary>
/// Author  :   Maikel van Munsteren
/// Desc    :   Container for all data for given tutorial
/// </summary>
public class TutorialModel
{
    private string _Title;
    private string _VideoPath;
    private string _TutorialText;

    public TutorialModel()
    {

    }

    public TutorialModel(string path)
    {
        SetData(path);
    }

    private void SetData(string path)
    {
        _Title = AmcUtilities.ReadFileItem("title", path);
        _VideoPath = AmcUtilities.ReadFileItem("video", path);
        _TutorialText = AmcUtilities.ReadFileItem("text", path);
    }

    public string Title
    {
        get { return _Title; }
    }

    public string VideoPath
    {
        get { return _VideoPath; }
    }

    public string TutorialText
    {
        get { return _TutorialText; }
    }
}