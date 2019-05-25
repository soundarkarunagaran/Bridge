using Bridge.Test.NUnit;
using System;

namespace Bridge.ClientTest.Batch3.BridgeIssues
{
    [TestFixture(TestNameFormat = "#3970 - {0}")]
    public class Bridge3970
    {        
        [Test]
        public static void TestNamespaceAndClassSameName()
        {
            Assert.NotNull(NameBridge3970.App.Run());
        }
    }
}

namespace NameBridge3970
{
    public sealed class NameBridge3970 { }

    public sealed class App
    {
        public static SpecialList<CategorySetGroup> Run()
        {
            return SpecialList<CategorySetGroup>.Empty.Update(
                updater: pcs => pcs.With(
                    _ => _.CategorySets,

                    pcs.CategorySets.Update(
                        updater: cs => cs
                    )
                )
            );
        }
    }

    public sealed class CategorySetGroup
    {
        public SpecialList<CategorySet> CategorySets { get; }

        public CategorySetGroup With<TProp>(Func<CategorySetGroup, TProp> propIdentifier, TProp newValue) { return this; }
    }

    public sealed class CategorySet
    {
        public SpecialList<Category> Categories { get; }

        public CategorySet With<TProp>(Func<CategorySet, TProp> propIdentifier, TProp newValue) { return this; }
    }

    public sealed class Category { }

    public sealed class SpecialList<T>
    {
        public static SpecialList<T> Empty { get; } = new SpecialList<T>();

        public SpecialList<T> Update(Func<T, T> updater)
        {
            return this;
        }
    }
}
