using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using ServiceHub.Services.Interfaces;
using ServiceHub.ViewModels.Interfaces;
using ServiceHub.ViewModels.Text;

namespace ServiceHub.ViewModels.Custom
{
    public class CustomBuilder : IDataViewModelBuilder
    {
        public bool TryBuild(string data, [NotNullWhen(true)] out IDataViewModel? dataViewModel)
        {
            dataViewModel = null;

            try
            {
                JsonNode jsonNode = JsonNode.Parse(data)!;
                if (jsonNode == null)
                    return false;

                dataViewModel = BuildRoot(jsonNode);
                return dataViewModel != null;
            }
            catch
            {
                return false;
            }
        }

        private IDataViewModel BuildRoot(JsonNode node)
        {
            switch (node)
            {
                case JsonValue jValue:
                    return new RootViewModel(Build(jValue));
                case JsonObject jObject:
                    return new RootViewModel(Build(jObject, "Root"));
                case JsonArray jArray:
                    IEnumerable<IDataViewModel> list = GetItems(jArray);
                    return new RootViewModel(list);
                default:
                    return new TextViewModelBase(node.ToString());
            }
        }

        private IDataViewModel Build(JsonNode node, string name)
        {
            switch (node)
            {
                case JsonValue jValue:
                    return Build(jValue);
                case JsonArray jArray:
                    return Build(jArray, name);
                case JsonObject jObject:
                    return Build(jObject, name);
                default:
                    return new TextViewModelBase(node.ToString());
            }
        }

        private TextViewModelBase Build(JsonValue jsonValue)
        {
            return new TextViewModelBase(jsonValue.ToString());
        }

        private ItemsViewModel Build(JsonArray jArray, string name)
        {
            IEnumerable<IDataViewModel> collection = GetItems(jArray);

            return new ItemsViewModel(collection, name);
        }

        private IDataViewModel Build(JsonObject jObject, string name)
        {
            List<IDataViewModel> list = new List<IDataViewModel>();

            foreach (var item in jObject)
            {
                if (item.Value != null)
                {
                    IDataViewModel valueViewModel = Build(item.Value, item.Key);
                    IDataViewModel viewModel = valueViewModel;
                    if (valueViewModel is not ItemsViewModel)
                        viewModel = new KeyValueViewModel(item.Key, valueViewModel);
                    list.Add(viewModel);
                }
                else
                {
                    list.Add(new TextViewModelBase(item.ToString()));
                }
            }

            return new ItemsViewModel(list, name);
        }

        private IEnumerable<IDataViewModel> GetItems(JsonArray jArray)
        {
            List<IDataViewModel> list = new List<IDataViewModel>();

            for (int i = 0; i < jArray.Count; i++)
            {
                JsonNode? item = jArray[i];
                if (item != null)
                    list.Add(Build(item, $"[{i}]"));
            }

            return list;
        }
    }
}
