using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodDelivery.Models.DominModels.Auth;

public class ChiefSignUpModel : SignUpModel
{
    [StringLength(100)]
    public string MiddleName { get; set; }

    public string GovernmentID { get; set; }
}
