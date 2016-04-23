using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace DAL
{
    //    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            var autoDetectChangesEnabled = context.Configuration.AutoDetectChangesEnabled;
            // much-much faster for bulk inserts!!!!
            context.Configuration.AutoDetectChangesEnabled = false;

            SeedIdentity(context);
            SeedArticles(context);
            SeedContacts(context);
            SeedExerciseTypes(context);


            // restore state
            context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;

            base.Seed(context);
        }

        private void SeedContacts(DataBaseContext context)
        {
            context.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = new MultiLangString("Skype", "en", "Skype", "ContactType.0")
            });
            context.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = new MultiLangString("Email", "en", "Email", "ContactType.0")
            });
            context.ContactTypes.Add(new ContactType()
            {
                ContactTypeName = new MultiLangString("Phone", "en", "Phone", "ContactType.0")
            });

            context.SaveChanges();

            // fill database with random names and data

            //undocumented way to get directory
            var path = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            string lastNamesFull = null;
            string firstNamesFull = null;
            string middleNamesFull = null;
            string placesFull = null;
            string countriesFull = null;
            
            if (File.Exists(path + "\\names.json"))
            {
                lastNamesFull = File.ReadAllText(path + "\\names.json");
            }
            if (File.Exists(path + "\\countries.json"))
            {
                countriesFull = File.ReadAllText(path + "\\countries.json");
            }
            if (File.Exists(path + "\\first-names.json"))
            {
                firstNamesFull = File.ReadAllText(path + "\\first-names.json");
            }
            if (File.Exists(path + "\\middle-names.json"))
            {
                middleNamesFull = File.ReadAllText(path + "\\middle-names.json");
            }
            if (File.Exists(path + "\\places.json"))
            {
                placesFull = File.ReadAllText(path + "\\places.json");
            }

            var jsonObjCountries = JObject.Parse(countriesFull);
            var jsonArrayCountries = (JArray) jsonObjCountries["countries"]["country"];
            List<string> countryList = jsonArrayCountries.Select(a => (string) a["countryName"]).ToList();

            List<string> placesList = JArray.Parse(placesFull).ToObject<List<string>>();
            List<string> lastNamesList = JArray.Parse(lastNamesFull).ToObject<List<string>>();
            List<string> firstNamesList = JArray.Parse(firstNamesFull).ToObject<List<string>>();
            List<string> middleNamesList = JArray.Parse(middleNamesFull).ToObject<List<string>>();

            Random r = new Random();


            var skypeId = context.ContactTypes.FirstOrDefault(a => a.ContactTypeName.Value == "Skype")?.ContactTypeId ?? 0;
            var emailId = context.ContactTypes.FirstOrDefault(a => a.ContactTypeName.Value == "Email")?.ContactTypeId ?? 0;
            var userId = context.UsersInt.FirstOrDefault(a => a.Email == "1@eesti.ee")?.Id ?? 0;

            var counter = 0;
            foreach (var lastName in lastNamesList.Take(200))
            {
                var firstName = firstNamesList[r.Next(0, firstNamesList.Count)];

                context.Persons.Add(new Person()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    UserId = userId,

                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            ContactTypeId = skypeId, 
                            ContactValue = middleNamesList[r.Next(0, middleNamesList.Count)]+"."+placesList[r.Next(0, placesList.Count)]
                        },
                        new Contact()
                        {
                            ContactTypeId = emailId, 
                            ContactValue = firstName+"."+lastName+"@"+countryList[r.Next(0, countryList.Count)]+".com"
                        }
                    },

                    Date = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0,365*50))),
                    Time = DateTime.Now.Subtract(TimeSpan.FromMinutes(r.Next(0, 12*60))),
                    DateTime = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0, 365 * 50))),
                    Date2 = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0, 365 * 50))),
                    Time2 = DateTime.Now.Subtract(TimeSpan.FromMinutes(r.Next(0, 12*60))),
                    DateTime2 = DateTime.Now.Subtract(TimeSpan.FromDays(r.Next(0, 365 * 50))),
                });

                //Save after every X records
                counter++;
                if (counter % 100 == 0)
                    context.SaveChanges();
            }
            // save the remaining
            context.SaveChanges();

        }

        private void SeedArticles(DataBaseContext context)
        {
            var articleHeadLine = "<h1>ASP.NET</h1>";
            var articleBody =
                "<p class=\"lead\">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.<br/>" +
                "As a demo, here is simple Contact application - log in and save your contacts!</p>";
            var article = new Article()
            {
                ArticleName = "HomeIndex",
                ArticleHeadline =
                    new MultiLangString(articleHeadLine, "en", articleHeadLine, "Article.HomeIndex.ArticleHeadline"),
                ArticleBody = new MultiLangString(articleBody, "en", articleBody, "Article.HomeIndex.ArticleBody")
            };
            context.Articles.Add(article);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "<h1>ASP.NET on suurepärane!</h1>",
                Culture = "et",
                MultiLangString = article.ArticleHeadline
            });

            context.Translations.Add(new Translation()
            {
                Value =
                    "<p class=\"lead\">ASP.NET on tasuta veebiraamistik suurepäraste veebide arendamiseks kasutades HTML-i, CSS-i, ja JavaScript-i.<br/>" +
                    "Demona on siin lihtne Kontaktirakendus - logi sisse ja salvesta enda kontakte</p>",
                Culture = "et",
                MultiLangString = article.ArticleBody
            });
            context.SaveChanges();
        }

        private void SeedIdentity(DataBaseContext context)
        {
            var pwdHasher = new PasswordHasher();

            // Roles
            context.RolesInt.Add(new RoleInt()
            {
                Name = "Admin"
            });

            context.SaveChanges();

            // Users
            context.UsersInt.Add(new UserInt()
            {
                UserName = "1@eesti.ee",
                Email = "1@eesti.ee",
                FirstName = "Super",
                LastName = "User",
                PasswordHash = pwdHasher.HashPassword("a"),
                SecurityStamp = Guid.NewGuid().ToString()
            });

            context.SaveChanges();

            // Users in Roles
            context.UserRolesInt.Add(new UserRoleInt()
            {
                User = context.UsersInt.FirstOrDefault(a => a.UserName == "1@eesti.ee"),
                Role = context.RolesInt.FirstOrDefault(a => a.Name == "Admin")
            });

            context.SaveChanges();
        }

        private void SeedExerciseTypes(DataBaseContext context)
        {
            var exerciseType1 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Quadriceps", "en", "Quadriceps", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Quadriceps training exercise.", "en", "Quadriceps training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType1);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Reie-nelipealihas",
                Culture = "et",
                MultiLangString = exerciseType1.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Reie-nelipealihase treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType1.ExerciseTypeDescription
            });
            context.SaveChanges();

            var exerciseType2 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Hamstrings", "en", "Hamstrings", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Hamstrings training exercise.", "en", "Hamstrings training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType2);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Reie tagaosa",
                Culture = "et",
                MultiLangString = exerciseType2.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Reie tagaosa treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType2.ExerciseTypeDescription
            });
            context.SaveChanges();

            var exerciseType3 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Calves", "en", "Calves", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Calves training exercise.", "en", "Calves training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType3);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Sääred",
                Culture = "et",
                MultiLangString = exerciseType3.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Säärte treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType3.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType4 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Hips", "en", "Hips", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Hips training exercise.", "en", "Hips training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType4);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Puusad",
                Culture = "et",
                MultiLangString = exerciseType4.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Puusade treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType4.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType5 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Pectorals", "en", "Pectorals", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Pectorals training exercise.", "en", "Pectorals training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType5);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Rinnalihas",
                Culture = "et",
                MultiLangString = exerciseType5.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Rinnalihaste treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType5.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType6 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Lats", "en", "Lats", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Lats training exercise.", "en", "Lats training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType6);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Seljalihas",
                Culture = "et",
                MultiLangString = exerciseType6.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Selja lailihase treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType6.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType7 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Deltoids", "en", "Deltoids", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Deltoids training exercise.", "en", "Deltoids training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType7);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Deltalihas",
                Culture = "et",
                MultiLangString = exerciseType7.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Deltalihase treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType7.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType8 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Triceps", "en", "Triceps", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Triceps training exercise.", "en", "Triceps training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType8);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Triitseps",
                Culture = "et",
                MultiLangString = exerciseType8.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Triitsepsi treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType8.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType9 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Biceps", "en", "Biceps", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Biceps training exercise.", "en", "Biceps training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType9);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Biitseps",
                Culture = "et",
                MultiLangString = exerciseType9.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Biitsepsi treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType9.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType10 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Abdominals", "en", "Abdominals", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Abdominals training exercise.", "en", "Abdominals training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType10);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Kõhulihased",
                Culture = "et",
                MultiLangString = exerciseType10.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Kõhulihaste treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType10.ExerciseTypeDescription
            });
            context.SaveChanges();


            var exerciseType11 = new ExerciseType()
            {
                ExerciseTypeName = new MultiLangString("Lower back", "en", "Lower back", "ExerciseType.Name"),
                ExerciseTypeDescription = new MultiLangString("Lower back training exercise.", "en", "Lower back training exercise.", "ExerciseType.Description")
            };

            context.ExerciseTypes.Add(exerciseType11);
            context.SaveChanges();

            context.Translations.Add(new Translation()
            {
                Value = "Alaselg",
                Culture = "et",
                MultiLangString = exerciseType11.ExerciseTypeName
            });

            context.Translations.Add(new Translation()
            {
                Value = "Alaselja treenimise harjutus",
                Culture = "et",
                MultiLangString = exerciseType11.ExerciseTypeDescription
            });
            context.SaveChanges();


        }
    }
}