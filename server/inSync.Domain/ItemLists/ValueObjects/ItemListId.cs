namespace inSync.Application.ItemLists.ValueObjects;
    public record ItemListId{
		public Guid Value {get; init;}
		private ItemListId(Guid id){
			Value = id;
		}
		public static ItemListId Create(){
			return new ItemListId(Guid.NewGuid());
		} 
		public static ItemListId Create(Guid id){
			return new ItemListId(id);
		}
	}

