namespace MVCBeginner.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Resources = MVCBeginner.Properties.Resources;

    /// <summary>
    ///     User Model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        ///     Gets or sets FirstName
        /// </summary>
        [Display(Name = nameof(Resources.FirstName), ResourceType = typeof(Resources))]
        [StringLength(100, MinimumLength = 4)]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
 ErrorMessageResourceType = typeof(Resources))]
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets LastName
        /// </summary>
        [StringLength(50)]
        [Display(Name = nameof(Resources.LastName), ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
 ErrorMessageResourceType = typeof(Resources))]
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets EmailID
        /// </summary>
        [StringLength(50)]
        [Display(Name = nameof(Resources.EmailID), ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
 ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }

        /// <summary>
        ///     Gets or sets UserRole
        /// </summary>
        [Display(Name = nameof(Resources.UserRole), ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
 ErrorMessageResourceType = typeof(Resources))]
        public Role UserRole { get; set; }

        /// <summary>
        ///     Gets or sets DateOfBirth
        /// </summary>
        [Display(Name = nameof(Resources.DateOfBirth), ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///     Gets or sets Password
        /// </summary>
        [Display(Name = nameof(Resources.Password), ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
 ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [MaxLength(20)]
        [MinLength(6)]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets ConfirmPassword
        /// </summary>
        [Display(Name = nameof(Resources.ConfirmPassword), ResourceType = typeof(Resources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = nameof(Resources.FieldRequired),
 ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.Password)]
        [Compare(nameof(Resources.Password), ErrorMessageResourceName = nameof(Resources.PassMisMatchErrMsg),
 ErrorMessageResourceType = typeof(Resources))]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    ///     Enum for Role
    /// </summary>
    public enum Role { Admin, Normal }
}