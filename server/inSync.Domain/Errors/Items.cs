public partial class Errors {

    public static class Items {
        public static readonly Error ItemDoesNotExist = new Error(nameof(ItemDoesNotExist), "Item does not exist");
    }
}