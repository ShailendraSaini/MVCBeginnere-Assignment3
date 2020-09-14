namespace MVCBeginner.Models
{
    using System.ComponentModel.DataAnnotations;
    using Resources = MVCBeginner.Properties.Resources;

    /// <summary>
    ///     Friend Model
    /// </summary>
    public class Friend
    {
        /// <summary>
        ///     Gets or sets FriendID
        /// </summary>
        [Required]
        [Display(Name = nameof(Resources.FriendID), ResourceType = typeof(Resources))]
        public int FriendID { get; set; }

        /// <summary>
        ///     Gets or sets FriendName
        /// </summary>
        [StringLength(50)]
        [Display(Name = nameof(Resources.FriendName), ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
ErrorMessageResourceType = typeof(Resources))]
        public string FriendName { get; set; }

        /// <summary>
        ///     Gets or sets Place
        /// </summary>
        [StringLength(25)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
ErrorMessageResourceType = typeof(Resources))]
        public string Place { get; set; }
    }
}