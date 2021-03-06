﻿using System.ComponentModel.DataAnnotations;

namespace BookShop.ViewModels.Authors
{
	public class CreateOrUpdateAuthorViewModel: BaseViewModel<int>
	{
		[Required(ErrorMessage = "Please input your name")]
		public string Name { get; set; }

		public string Alias { set; get; }
		public string Description { get; set; }
	}
}
