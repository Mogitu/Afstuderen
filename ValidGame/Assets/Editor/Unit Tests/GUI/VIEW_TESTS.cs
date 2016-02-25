using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using AMC.GUI;

[TestFixture]
[Category("GUI tests")]
internal class VIEW_TESTS {

    [Test]
    public void SetPresenter_NOTNULL()
    {
        IView view = NSubstitute.Substitute.For<IView>();
        IPresenter presenter = NSubstitute.Substitute.For<Presenter>();
        view.SetPresenter(presenter);
        Assert.That(presenter != null);
    }
}
