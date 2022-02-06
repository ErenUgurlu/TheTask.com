using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CodersNature.Models;

// Add profile data for application users by adding properties to the CodersNatureUser class
public class CodersNatureUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Hometown { get; set; }
    [NotMapped]
    public Cities City { get; set; }
}

public enum Cities { Adana = 1, Adıyaman = 2, Afyonkarahisar = 3, Ağrı = 4, Aksaray = 5, Amasya = 6, Ankara = 7, Antalya = 8, Ardahan = 9, Artvin = 10, Aydın = 11, Balıkesir = 12, Bartın = 13, Batman = 14, Bayburt = 15, Bilecik = 16, Bingol = 17, Bitlis = 18, Bolu = 19, Burdur = 20, Bursa = 21, Çanakkale = 22, Çankırı = 23, Çorum = 24, Denizli = 25, Diyarbakır = 26, Düzce = 27, Edirne = 28, Elaziğ = 29, Erzincan = 30, Erzurum = 31, Eskişehir = 32, Gaziantep = 33, Giresun = 34, Gümüşhane = 35, Hakkâri = 36, Hatay = 37, Iğdır = 38, Isparta = 39, istanbul = 40, izmir = 41, Kahramanmaraş = 42, Karabük = 43, Karaman = 44, Kars = 45, Kastamonu = 46, Kayseri = 47, Kırıkkale = 48, Kırklareli = 49, Kırsehir = 50, Kilis = 51, Kocaeli = 52, Konya = 53, Kütahya = 54, Malatya = 55, Manisa = 56, Mardin = 57, Mersin = 58, Muğla = 59, Muş = 60, Nevşehir = 61, Niğde = 62, Ordu = 63, Osmaniye = 64, Rize = 65, Sakarya = 66, Samsun = 67, Siirt = 68, Sinop = 69, Sivas = 70, Şanlıurfa = 71, Şırnak = 72, Tekirdağ = 73, Tokat = 74, Trabzon = 75, Tunceli = 76, Uşak = 77, Van = 78, Yalova = 79, Yozgat = 80, Zonguldak = 81 }

