//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Security.Cryptography;
//using System.Web.Mvc;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.draw;
//using Oracle.ManagedDataAccess.Client;


//namespace Library.Models.Account
//{
//    public class AccountModel : Model
//        {
//            public string Auth(string login, string password)
//            {
//                using (Connection)
//                {
//                    if (Session["login"] == null)
//                    {
//                        Connection.Open();
//                        var adapter = new OracleDataAdapter
//                        {
//                            SelectCommand = new OracleCommand
//                            {
//                                Connection = Connection,
//                                CommandText = "select id_user, is_admin, is_instr, blocked from users where login = :log and password = :pass"
//                            }
//                        };
//                        adapter.SelectCommand.Parameters.Add("log", OracleDbType.Varchar2).Value = login;
//                        var md5 = MD5.Create();
//                        adapter.SelectCommand.Parameters.Add("pass", OracleDbType.Varchar2).Value = GetMd5Hash(md5, password);
//                        var reader = adapter.SelectCommand.ExecuteReader();
//                        if (reader.HasRows)
//                        {
//                            while (reader.Read())
//                            {
//                                Session["id"] = reader["id_user"].ToString();
//                                if (reader["is_admin"].ToString() == "1")
//                                    Session["admin"] = true;
//                                if (reader["is_instr"].ToString() == "1")
//                                    Session["coach"] = true;
//                                if (reader["blocked"].ToString() == "1")
//                                    Session["block"] = true;
//                            }
//                            Session["login"] = true;
//                            Connection.Close();
//                            return "С возвращением, путник!";
//                        }
//                        else
//                        {
//                            Connection.Close();
//                            return "Неправильный логин или пароль!";
//                        }
//                    }
//                    else
//                    {
//                        Connection.Close();
//                        return "Вы уже авторизованы!";
//                    }
//                }
//            }

//            public string Register(string fio, string phone, string sex, string birthday, string login, string password)
//            {
//                using (Connection)
//                {
//                    if (Session["login"] == null)
//                    {
//                        Connection.Open();

//                        var adapter = new OracleDataAdapter
//                        {
//                            SelectCommand = new OracleCommand
//                            {
//                                Connection = Connection,
//                                CommandText = "select id_user from users where login = :log"
//                            }
//                        };
//                        adapter.SelectCommand.Parameters.Add("log", OracleDbType.Varchar2).Value = login;
//                        var reader = adapter.SelectCommand.ExecuteReader();

//                        if (!reader.HasRows)
//                        {
//                            adapter = new OracleDataAdapter
//                            {
//                                InsertCommand = new OracleCommand
//                                {
//                                    Connection = Connection,
//                                    CommandText = "insert into users (fio, phone, sex, birthday, login, password) values (:fio, :phone, :sex, :birthday, :login, :password)"
//                                }
//                            };
//                            adapter.InsertCommand.Parameters.Add("fio", OracleDbType.Varchar2).Value = fio;
//                            adapter.InsertCommand.Parameters.Add("phone", OracleDbType.Varchar2).Value = phone;
//                            adapter.InsertCommand.Parameters.Add("sex", OracleDbType.Varchar2).Value = sex;
//                            adapter.InsertCommand.Parameters.Add("birthday", OracleDbType.Date).Value = DateTime.Parse(birthday);
//                            adapter.InsertCommand.Parameters.Add("login", OracleDbType.Varchar2).Value = login;
//                            var md5 = MD5.Create();
//                            adapter.InsertCommand.Parameters.Add("password", OracleDbType.Varchar2).Value = GetMd5Hash(md5, password);
//                            adapter.InsertCommand.ExecuteReader();

//                            var idTourist = "";
//                            adapter = new OracleDataAdapter
//                            {
//                                SelectCommand = new OracleCommand
//                                {
//                                    Connection = Connection,
//                                    CommandText = "select max(id_user) as id from users"
//                                }
//                            };
//                            reader = adapter.SelectCommand.ExecuteReader();
//                            while (reader.Read())
//                            {
//                                idTourist = reader["id"].ToString();
//                            }

//                            Session["id"] = idTourist;
//                            Session["login"] = true;
//                            Connection.Close();
//                            return "Вы зарегистрированы! Удачи в пути!";
//                        }
//                        else
//                        {
//                            Connection.Close();
//                            return "Этот логин занят!";
//                        }
//                    }
//                    else
//                    {
//                        Connection.Close();
//                        return "Вы уже авторизованы!";
//                    }
//                }
//            }

//            public ProfileModel GetProfile(object tourist)
//            {
//                using (Connection)
//                {
//                    Connection.Open();
//                    var profile = new ProfileModel
//                    {
//                        Tourist = new TouristModel()
//                    };
//                    var user = profile.Tourist;
//                    var adapter = new OracleDataAdapter
//                    {
//                        SelectCommand = new OracleCommand
//                        {
//                            Connection = Connection,
//                            CommandText = "select login, fio, phone, sex, birthday from users where id_user = :id"
//                        }
//                    };
//                    adapter.SelectCommand.Parameters.Add("id", int.Parse(tourist.ToString()));
//                    var reader = adapter.SelectCommand.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        user.Birthday = reader["birthday"].ToString().Split(' ')[0];
//                        user.Fio = reader["fio"].ToString();
//                        user.Login = reader["login"].ToString();
//                        user.Phone = reader["phone"].ToString();
//                        user.Sex = reader["sex"].ToString();
//                    }
//                    Connection.Close();
//                    return profile;
//                }
//            }

//            public List<SelectListItem> GetHikesList()
//            {
//                using (Connection)
//                {
//                    Connection.Open();
//                    var adapter = new OracleDataAdapter
//                    {
//                        SelectCommand = new OracleCommand
//                        {
//                            Connection = Connection,
//                            CommandText = "select h.id_hike, hike_name, date_start, category_name from hikes h, categories c, experience e "
//                            + "where h.id_category = c.id_category and h.id_hike = e.id_hike and id_user != :id and done = 1 "
//                            + "and h.id_hike not in (select id_hike from experience where done = 1 and id_user = :id)"
//                        }
//                    };
//                    adapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = int.Parse(Session["id"].ToString());
//                    var reader = adapter.SelectCommand.ExecuteReader();
//                    var list = new List<SelectListItem>();
//                    while (reader.Read())
//                    {
//                        var item = new SelectListItem
//                        {
//                            Value = reader["id_hike"].ToString(),
//                            Text = reader["hike_name"] + " '" + reader["date_start"].ToString().Split(' ')[0].Split('.')[2].Substring(2, 2) + " " + reader["category_name"]
//                        };
//                        list.Add(item);
//                    }
//                    Connection.Close();
//                    return list;
//                }
//            }

//            public List<HikeModel> GetExpList(int id)
//            {
//                using (Connection)
//                {
//                    Connection.Open();
//                    var adapter = new OracleDataAdapter
//                    {
//                        SelectCommand = new OracleCommand
//                        {
//                            Connection = Connection,
//                            CommandText = "select is_lead, hike_name, date_start, date_finish, category_name, type_name "
//                            + "from hikes h, experience e, categories c, hiketypes t "
//                            + "where e.id_hike = h.id_hike and h.id_category = c.id_category and h.id_type = t.id_type "
//                            + "and id_user = :id and done = 1"
//                        }
//                    };
//                    adapter.SelectCommand.Parameters.Add("id", OracleDbType.Int32).Value = id;
//                    var reader = adapter.SelectCommand.ExecuteReader();
//                    var list = new List<HikeModel>();
//                    while (reader.Read())
//                    {
//                        var item = new HikeModel
//                        {
//                            IsLead = reader["is_lead"].ToString() == "1" ? "Р" : "У",
//                            Name = reader["hike_name"].ToString(),
//                            Start = reader["date_start"].ToString().Split(' ')[0],
//                            Finish = reader["date_finish"].ToString().Split(' ')[0],
//                            Category = reader["category_name"].ToString(),
//                            Type = reader["type_name"].ToString()
//                        };
//                        list.Add(item);
//                    }
//                    Connection.Close();
//                    return list;
//                }
//            }

//            public string UpdateProfile(ProfileModel profile, object id)
//            {
//                var user = profile.Tourist;
//                var idUser = int.Parse(id.ToString());
//                var resp = "";
//                using (Connection)
//                {
//                    Connection.Open();
//                    var adapter = new OracleDataAdapter
//                    {
//                        SelectCommand = new OracleCommand
//                        {
//                            Connection = Connection,
//                            CommandText = "select login, password, fio, phone, sex, birthday from users where id_user = :id"
//                        }
//                    };
//                    adapter.SelectCommand.Parameters.Add("id", idUser);
//                    var reader = adapter.SelectCommand.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        if (reader["login"].ToString() != user.Login)
//                        {
//                            adapter = new OracleDataAdapter
//                            {
//                                UpdateCommand = new OracleCommand
//                                {
//                                    Connection = Connection,
//                                    CommandText = "update users set login = :login where id_user = :id"
//                                }
//                            };
//                            adapter.UpdateCommand.Parameters.Add("login", user.Login);
//                            adapter.UpdateCommand.Parameters.Add("id", idUser);
//                            adapter.UpdateCommand.ExecuteReader();
//                            resp += "Логин успешно изменён!" + "<br/>";
//                        }

//                        if (user.Password != null)
//                        {
//                            var pass = GetMd5Hash(MD5.Create(), user.Password);
//                            if (reader["password"].ToString() != pass)
//                            {
//                                adapter = new OracleDataAdapter
//                                {
//                                    UpdateCommand = new OracleCommand
//                                    {
//                                        Connection = Connection,
//                                        CommandText = "update users set password = :pass where id_user = :id"
//                                    }
//                                };
//                                adapter.UpdateCommand.Parameters.Add("pass", pass);
//                                adapter.UpdateCommand.Parameters.Add("id", idUser);
//                                adapter.UpdateCommand.ExecuteReader();
//                                resp += "Пароль успешно изменён!" + "<br/>";
//                            }
//                        }

//                        if (user.Fio != reader["fio"].ToString())
//                        {
//                            adapter = new OracleDataAdapter
//                            {
//                                UpdateCommand = new OracleCommand
//                                {
//                                    Connection = Connection,
//                                    CommandText = "update users set fio = :fio where id_user = :id"
//                                }
//                            };
//                            adapter.UpdateCommand.Parameters.Add("fio", user.Fio);
//                            adapter.UpdateCommand.Parameters.Add("id", idUser);
//                            adapter.UpdateCommand.ExecuteReader();
//                            resp += "ФИО успешно изменены!" + "<br/>";
//                        }

//                        if (user.Phone != reader["phone"].ToString())
//                        {
//                            adapter = new OracleDataAdapter
//                            {
//                                UpdateCommand = new OracleCommand
//                                {
//                                    Connection = Connection,
//                                    CommandText = "update users set phone = :phone where id_user = :id"
//                                }
//                            };
//                            adapter.UpdateCommand.Parameters.Add("phone", user.Phone);
//                            adapter.UpdateCommand.Parameters.Add("id", idUser);
//                            adapter.UpdateCommand.ExecuteReader();
//                            resp += "Номер телефона успешно изменён!" + "<br/>";
//                        }

//                        if (user.Sex != reader["sex"].ToString())
//                        {
//                            adapter = new OracleDataAdapter
//                            {
//                                UpdateCommand = new OracleCommand
//                                {
//                                    Connection = Connection,
//                                    CommandText = "update users set sex = :sex where id_user = :id"
//                                }
//                            };
//                            adapter.UpdateCommand.Parameters.Add("sex", user.Sex);
//                            adapter.UpdateCommand.Parameters.Add("id", idUser);
//                            adapter.UpdateCommand.ExecuteReader();
//                            resp += "Пол успешно изменён!" + "<br/>";
//                        }

//                        if (DateTime.Parse(user.Birthday) != DateTime.Parse(reader["birthday"].ToString()))
//                        {
//                            adapter = new OracleDataAdapter
//                            {
//                                UpdateCommand = new OracleCommand
//                                {
//                                    Connection = Connection,
//                                    CommandText = "update users set birthday = :birthday where id_user = :id"
//                                }
//                            };
//                            adapter.UpdateCommand.Parameters.Add("birthday", user.Birthday);
//                            adapter.UpdateCommand.Parameters.Add("id", idUser);
//                            adapter.UpdateCommand.ExecuteReader();
//                            resp += "День рождения успешно изменён!" + "<br/>";
//                        }
//                    }
//                    Connection.Close();

//                    if (resp == "")
//                        resp = "Введите новые данные!";

//                    return resp;
//                }
//            }

//            public byte[] GetPdf()
//            {
//                var stream = new MemoryStream();
//                var doc = new Document(PageSize.A4, 50, 50, 50, 50);

//                var fg = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
//                var fgBaseFont = BaseFont.CreateFont(fg, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
//                var font = new Font(fgBaseFont, 14, Font.NORMAL, BaseColor.BLACK);

//                PdfWriter.GetInstance(doc, stream);
//                doc.Open();

//                using (Connection)
//                {
//                    Connection.Open();

//                    var fio = "";
//                    var adapter = new OracleDataAdapter
//                    {
//                        SelectCommand = new OracleCommand
//                        {
//                            Connection = Connection,
//                            CommandText = "select fio from users where id_user = :id_user"
//                        }
//                    };
//                    adapter.SelectCommand.Parameters.Add("id_user", OracleDbType.Int32).Value = Session["id"];
//                    var reader = adapter.SelectCommand.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        fio = reader["fio"].ToString();
//                    }

//                    var p = new Paragraph("ФИО туриста: " + fio, font);
//                    var glue = new Chunk(new VerticalPositionMark());
//                    p.Add(new Chunk(glue));
//                    p.Add("Дата распечатки: " + DateTime.Today.ToString("dd.MM.yy"));
//                    doc.Add(p);
//                    p = new Paragraph("Туристский опыт") { Alignment = Element.ALIGN_CENTER, Font = font };
//                    doc.Add(p);

//                    adapter = new OracleDataAdapter
//                    {
//                        SelectCommand = new OracleCommand
//                        {
//                            Connection = Connection,
//                            CommandText = "select distinct extract(year from date_start) as year from hikes order by year desc"
//                        }
//                    };
//                    reader = adapter.SelectCommand.ExecuteReader();

//                    while (reader.Read())
//                    {
//                        var year = reader["year"].ToString();
//                        p = new Paragraph(year, font);
//                        doc.Add(p);
//                        var table = new PdfPTable(3);
//                        table.AddCell(GetCell("Название похода"));
//                        table.AddCell(GetCell("Категория сложности"));
//                        table.AddCell(GetCell("Сроки проведения"));

//                        var adapter2 = new OracleDataAdapter
//                        {
//                            SelectCommand = new OracleCommand
//                            {
//                                Connection = Connection,
//                                CommandText = "select hike_name, category_name, date_start, date_finish from categories c, hikes h, experience ex "
//                                + "where h.id_category = c.id_category and ex.id_hike = h.id_hike and done = 1 and id_user = :id_user "
//                                + "and extract(year from date_start) = :year"
//                            }
//                        };
//                        adapter2.SelectCommand.Parameters.Add("id_user", OracleDbType.Int32).Value = int.Parse(Session["id"].ToString());
//                        adapter2.SelectCommand.Parameters.Add("year", OracleDbType.Int32).Value = int.Parse(year);
//                        var hikes = adapter2.SelectCommand.ExecuteReader();

//                        while (hikes.Read())
//                        {
//                            table.AddCell(GetCell(hikes["hike_name"].ToString()));
//                            table.AddCell(GetCell(hikes["category_name"].ToString()));
//                            table.AddCell(GetCell(hikes["date_start"].ToString().Split(' ')[0] + " - " +
//                                                  hikes["date_finish"].ToString().Split(' ')[0]));
//                        }

//                        doc.Add(table);
//                    }

//                    Connection.Close();
//                }

//                doc.Close();
//                return stream.ToArray();
//            }

//            private static PdfPCell GetCell(string text)
//            {
//                var fg = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALUNI.TTF");
//                var fgBaseFont = BaseFont.CreateFont(fg, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
//                var font = new Font(fgBaseFont, 14, Font.NORMAL, BaseColor.BLACK);
//                var cell = new PdfPCell
//                {
//                    Padding = 5,
//                    Phrase = new Phrase(text, font),
//                    HorizontalAlignment = 1,
//                };
//                return cell;
//            }

//            public string AddExperience(ExperienceModel exp)
//            {
//                using (Connection)
//                {
//                    Connection.Open();
//                    var adapter = new OracleDataAdapter
//                    {
//                        InsertCommand = new OracleCommand
//                        {
//                            Connection = Connection,
//                            CommandText = "insert into experience (id_user, id_hike, is_lead, accept, done) "
//                            + "values (:id_user, :id_hike, :is_lead, :accept, :done)"
//                        }
//                    };
//                    adapter.InsertCommand.Parameters.Add("id_user", OracleDbType.Int32).Value = exp.IdUser;
//                    adapter.InsertCommand.Parameters.Add("id_hike", OracleDbType.Int32).Value = exp.IdHike;
//                    adapter.InsertCommand.Parameters.Add("is_lead", OracleDbType.Char).Value = '0';
//                    adapter.InsertCommand.Parameters.Add("accept", OracleDbType.Char).Value = '1';
//                    adapter.InsertCommand.Parameters.Add("done", OracleDbType.Char).Value = "1";
//                    try
//                    {
//                        adapter.InsertCommand.ExecuteNonQuery();
//                        return "Пройденный поход добавлен в базу данных!";
//                    }
//                    catch (Exception e)
//                    {
//                        var ex = e.Message.Substring(11);
//                        return ex.Remove(ex.IndexOf("ORA", StringComparison.CurrentCulture));
//                    }
//                    finally
//                    {
//                        Connection.Close();
//                    }
//                }
//            }
//        }
//    }
//}