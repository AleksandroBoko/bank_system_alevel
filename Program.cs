using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_system
{
    class Program
    {
        enum TypeUser
        {
            Admin,
            User,
            Guest
        }

        enum TypeCheckAccount
        {
            Login,
            Account
        }

        enum TypeMoneyOperation
        {
            Adding,
            Removing
        }

        static void Main(string[] args)
        {
            string[,] Users = new string[1, 3] { { "admin", "123", "0" } };
            string[,] UsersAccounts = new string[,] { };

            EnterBank(UsersAccounts, Users);

            Console.ReadKey();
        }

        /// <summary>
        /// Вход в банковскую систему
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void EnterBank(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("Enter your login:");
            string login = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            TypeUser TUser = GetTypeUser(login, password, CurUsers);

            if (TUser == TypeUser.User)
            {
                if (CheckNotBlockedStatus(CurUsers, login))
                    ShowUserMenu(CurUserAccounts, CurUsers, login);
                else
                {
                    Console.WriteLine("The account is blocked! Contact administrator of the bank system.");
                }
            }
            else if (TUser == TypeUser.Admin)
            {
                ShowAdminMenu(CurUserAccounts, CurUsers);
            }
            else
            {
                Console.WriteLine("The bank system couldn't recognize current user!");
                Console.WriteLine("Would you like to try one more time? - Press '0'");
                Console.WriteLine("Exit - Press any key");
                string answer = Console.ReadLine();
                if (answer == "0")
                    EnterBank(CurUsers, CurUserAccounts);
            }

        }

        /// <summary>
        /// Проверка на тип пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="CurUsers"></param>
        /// <returns></returns>
        static TypeUser GetTypeUser(string login, string password, string[,] CurUsers)
        {
            TypeUser TUser = TypeUser.Guest;
            for (int i = 0; i < CurUsers.GetLength(0); i++)
            {
                if (CurUsers[i, 0] == login && CurUsers[i, 1] == password)
                {
                    if (login == "admin" && password == "123")
                        TUser = TypeUser.Admin;
                    else
                        TUser = TypeUser.User;
                }
            }

            return TUser;
        }

        /// <summary>
        /// Проверка на отсутствие блокировки пользователя
        /// </summary>
        /// <param name="CurUsers"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        static bool CheckNotBlockedStatus(string[,] CurUsers, string login)
        {
            bool Result = false;
            for (int i = 0; i < CurUsers.GetLength(0); i++)
            {
                if (CurUsers[i, 0] == login)
                    Result = CurUsers[i, 2] == "0";
            }

            return Result;
        }

        /// <summary>
        /// Отображение меню администратора
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void ShowAdminMenu(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("----------Admin menu---------");
            Console.WriteLine("See user accounts - Press '0'");
            Console.WriteLine("Block user        - Press '1'");
            Console.WriteLine("Unblock user      - Press '2'");
            Console.WriteLine("Add new user      - Press '3'");
            Console.WriteLine("Add new account   - Press '4'");
            Console.WriteLine("Remove user       - Press '5'");
            Console.WriteLine("Remove account    - Press '6'");
            Console.WriteLine("Log out           - Press '7'");
            Console.WriteLine("Exit              - Press any key");

            string answer = Console.ReadLine();
            switch (answer)
            {
                case "0": ShowUserAccounts(CurUserAccounts, CurUsers); break;
                case "1": BlockUser(CurUserAccounts, CurUsers); break;
                case "2": UnBlockUser(CurUserAccounts, CurUsers); break;
                case "3": AddNewUser(CurUserAccounts, CurUsers); break;
                case "4": AddNewAccount(CurUserAccounts, CurUsers); break;
                case "5": RemoveUser(CurUserAccounts, CurUsers); break;
                case "6": RemoveAccount(CurUserAccounts, CurUsers); break;
                case "7": LogOut(CurUserAccounts, CurUsers); break;
            }
        }

        /// <summary>
        /// Отображаем пользовательское меню
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        /// <param name="UserName"></param>
        static void ShowUserMenu(string[,] CurUserAccounts, string[,] CurUsers, string UserName)
        {
            Console.Clear();
            Console.WriteLine("----------User menu----------");
            Console.WriteLine("See my accounts   - Press '0'");
            Console.WriteLine("Add money         - Press '1'");
            Console.WriteLine("Take money        - Press '2'");
            Console.WriteLine("Transfer money    - Press '3'");
            Console.WriteLine("Log out           - Press '4'");
            Console.WriteLine("Exit              - Press any key");

            string answer = Console.ReadLine();
            switch (answer)
            {
                case "0": ShowCurrentUserAccount(CurUserAccounts, CurUsers, UserName); break;
                case "1": AddMoney(CurUserAccounts, CurUsers, UserName); break;
                case "2": TakeMoney(CurUserAccounts, CurUsers, UserName); break;
                case "3": TransferMoney(CurUserAccounts, CurUsers, UserName); break;
                case "4": LogOut(CurUserAccounts, CurUsers); break;
            }
        }

        /// <summary>
        /// Отображаем список аккаунтов пользователя
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void ShowUserAccounts(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            string answer = "";
            if (CurUserAccounts.GetLength(0) > 0)
            {
                Console.WriteLine("Show whole list?           - Press '0'");
                Console.WriteLine("Show certain user account? - Press '1'");
                answer = Console.ReadLine();

                Console.Clear();

                bool hasUserAccount = false;
                if (answer == "0")
                {
                    Console.WriteLine("----------The list of users----------");
                    for (int j = 0; j < CurUsers.GetLength(0); j++)
                    {
                        hasUserAccount = false;
                        for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
                        {
                            if (CurUsers[j, 0] == CurUserAccounts[i, 0])
                            {
                                Console.WriteLine("{0} - {1} - {2} - Total money:{3} - History: {4}", CurUserAccounts[i, 0],
                                                                                                     GetUserStatus(CurUsers, CurUserAccounts[i, 0]),
                                                                                                     CurUserAccounts[i, 1],
                                                                                                     CurUserAccounts[i, 2],
                                                                                                     CurUserAccounts[i, 3]);
                                hasUserAccount = true;
                            }
                        }

                        if (!hasUserAccount)
                            Console.WriteLine("{0} - no account", CurUsers[j, 0]);
                    }
                }
                else if (answer == "1")
                {
                    Console.WriteLine("Write a user name");
                    string UserName = Console.ReadLine();

                    bool FindUser = false;

                    Console.Clear();
                    Console.WriteLine("----------The list of users----------");
                    for (int j = 0; j < CurUsers.GetLength(0); j++)
                    {
                        if (CurUsers[j, 0] == UserName)
                        {
                            for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
                            {
                                if (CurUsers[j, 0] == CurUserAccounts[i, 0])
                                {
                                    Console.WriteLine("{0} - {1} - {2} - Total money:{3} - History: {4}", CurUserAccounts[i, 0],
                                                                                                          GetUserStatus(CurUsers, CurUserAccounts[i, 0]),
                                                                                                          CurUserAccounts[i, 1],
                                                                                                          CurUserAccounts[i, 2],
                                                                                                          CurUserAccounts[i, 3]);
                                    hasUserAccount = true;
                                }
                            }

                            if (!hasUserAccount)
                                Console.WriteLine("{} - no account", CurUsers[j, 0]);

                            FindUser = true;
                            break;
                        }
                    }
                    if (!FindUser)
                        Console.WriteLine("Unfortunately, the user wasn't found!");
                }
                else
                {
                    Console.WriteLine("Unknown command!");
                }
            }
            else
            {
                Console.WriteLine("The list of user's accounts is empty!");
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            answer = Console.ReadLine();
            if (answer == "0")
                ShowAdminMenu(CurUserAccounts, CurUsers);
        }

        /// <summary>
        /// Получаем статус пользователя: 0 - разблокирован, 1 - заблокирован 
        /// </summary>
        /// <param name="CurUsers"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        static string GetUserStatus(string[,] CurUsers, string login)
        {
            string result = "";
            for (int i = 0; i < CurUsers.GetLength(0); i++)
            {
                if (login == CurUsers[i, 0])
                {
                    result = CurUsers[i, 2];
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void AddNewUser(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("---------Adding user---------");
            Console.WriteLine("Write a login of user:");
            string NameUser = Console.ReadLine();

            string[,] users = CurUsers;
            string[,] usersAccounts = CurUserAccounts;

            if (CheckUser(NameUser, CurUsers))
            {
                Console.WriteLine("This login is used! Write another one");
            }
            else
            {
                Console.WriteLine("Write an user's password:");
                string PasswordUser = Console.ReadLine();


                Console.WriteLine("Write number of user's account");
                string AccountUser = Console.ReadLine();
                if (CheckAccount(AccountUser, CurUserAccounts, TypeCheckAccount.Account))
                {
                    Console.WriteLine("You need to add another account. This is used!");
                }
                else
                {
                    users = AddUser(NameUser, PasswordUser, CurUsers);
                    usersAccounts = AddAccount(NameUser, AccountUser, CurUserAccounts);
                    Console.WriteLine("The user was added!");
                }
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowAdminMenu(usersAccounts, users);
        }

        /// <summary>
        /// Добавление нового пользовательского аккаунта
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void AddNewAccount(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("---------Adding user's account---------");

            Console.WriteLine("Write a name of user:");
            string NameUser = Console.ReadLine();

            string[,] usersAccounts = CurUserAccounts;
            if (CheckUser(NameUser, CurUsers))
            {
                Console.WriteLine("Write number of user's account");
                string AccountUser = Console.ReadLine();

                if (CheckAccount(AccountUser, CurUserAccounts, TypeCheckAccount.Account))
                {
                    Console.WriteLine("You need to add another account. This is used!");
                }
                else
                {
                    usersAccounts = AddAccount(NameUser, AccountUser, CurUserAccounts);
                    Console.WriteLine("The account was added!");
                }
            }
            else
            {
                Console.WriteLine("You set an unknown user!");
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowAdminMenu(usersAccounts, CurUsers);
        }

        /// <summary>
        /// Добавление пользователя в исходное хранилище
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="CurUsers"></param>
        /// <returns></returns>
        static string[,] AddUser(string login, string password, string[,] CurUsers)
        {
            string[,] NewCurUser = new string[CurUsers.GetLength(0) + 1, 3];

            for (int i = 0; i < CurUsers.GetLength(0); i++)
            {
                for (int j = 0; j < CurUsers.GetLength(1); j++)
                {
                    NewCurUser[i, j] = CurUsers[i, j];
                }
            }

            NewCurUser[NewCurUser.GetLength(0) - 1, 0] = login;
            NewCurUser[NewCurUser.GetLength(0) - 1, 1] = password;
            NewCurUser[NewCurUser.GetLength(0) - 1, 2] = "0";      // status 0 - unblocked, 1 - blocked
            return NewCurUser;
        }

        /// <summary>
        /// Добавление аккаунта в исходное хранилище
        /// </summary>
        /// <param name="login"></param>
        /// <param name="account"></param>
        /// <param name="CurUserAccounts"></param>
        /// <returns></returns>
        static string[,] AddAccount(string login, string account, string[,] CurUserAccounts)
        {
            string[,] NewCurAccount = new string[CurUserAccounts.GetLength(0) + 1, 4];

            for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
            {
                for (int j = 0; j < CurUserAccounts.GetLength(1); j++)
                {
                    NewCurAccount[i, j] = CurUserAccounts[i, j];
                }
            }

            NewCurAccount[NewCurAccount.GetLength(0) - 1, 0] = login;    // login
            NewCurAccount[NewCurAccount.GetLength(0) - 1, 1] = account;  // account
            NewCurAccount[NewCurAccount.GetLength(0) - 1, 2] = "0";      // money            
            NewCurAccount[NewCurAccount.GetLength(0) - 1, 3] = "";       // history of operations  
            return NewCurAccount;
        }

        /// <summary>
        /// Блокирование пользователя
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void BlockUser(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("---------Blocking user---------");
            Console.WriteLine("Write a name user, who you want to block:");
            string login = Console.ReadLine();

            if (CheckUser(login, CurUsers))
            {
                for (int i = 0; i < CurUsers.GetLength(0); i++)
                {
                    if (login == CurUsers[i, 0])
                    {
                        CurUsers[i, 2] = "1"; // блокируем пользователя
                        Console.WriteLine("You've blocked user successfully");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("The user isn't found");
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowAdminMenu(CurUserAccounts, CurUsers);
        }

        /// <summary>
        /// Проверка на существование пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="CurUsers"></param>
        /// <returns></returns>
        static bool CheckUser(string login, string[,] CurUsers)
        {
            bool result = false;
            for (int i = 0; i < CurUsers.GetLength(0); i++)
            {
                if (login == CurUsers[i, 0])
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Проверка на существование аккаунтов(счетов)
        /// </summary>
        /// <param name="CheckValue"></param>
        /// <param name="CurUserAccounts"></param>
        /// <param name="TypeCheck"></param>
        /// <returns></returns>
        static bool CheckAccount(string CheckValue, string[,] CurUserAccounts, TypeCheckAccount TypeCheck)
        {
            bool result = false;

            if (TypeCheck == TypeCheckAccount.Account) // Проверка на номер счета
            {
                for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
                {
                    if (CheckValue == CurUserAccounts[i, 1])
                    {
                        result = true;
                        break;
                    }
                }
            }
            else                                      // Проверка на логин пользователя
            {
                for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
                {
                    if (CheckValue == CurUserAccounts[i, 0])
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Проверка аккаунта(счета) перед удалением
        /// </summary>
        /// <param name="CheckValue"></param>
        /// <param name="CurUserAccounts"></param>
        /// <param name="TypeCheck"></param>
        /// <returns></returns>
        static bool CheckMoneyBeforeRemove(string CheckValue, string[,] CurUserAccounts, TypeCheckAccount TypeCheck)
        {
            bool result = false;

            if (TypeCheck == TypeCheckAccount.Account)
            {
                for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
                {
                    if (CheckValue == CurUserAccounts[i, 1] && int.Parse(CurUserAccounts[i, 2]) > 10)
                    {
                        result = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
                {
                    if (CheckValue == CurUserAccounts[i, 0] && int.Parse(CurUserAccounts[i, 2]) > 10)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Разблокирование пользователя
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void UnBlockUser(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("---------UnBlocking user---------");
            Console.WriteLine("Write a name user, who you want to unblock:");
            string login = Console.ReadLine();

            if (CheckUser(login, CurUsers))
            {
                for (int i = 0; i < CurUsers.GetLength(0); i++)
                {
                    if (login == CurUsers[i, 0])
                    {
                        CurUsers[i, 2] = "0"; // разблокируем пользователя
                        Console.WriteLine("You've unblocked user successfully");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("The user isn't found");
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowAdminMenu(CurUserAccounts, CurUsers);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void RemoveUser(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("---------Removing user---------");
            Console.WriteLine("Write a name user, who you want to remove:");
            string login = Console.ReadLine();

            string[,] NewUsers = CurUsers;
            string[,] NewUserAccounts = CurUserAccounts;

            if (CheckUser(login, CurUsers))
            {

                bool ResultOfRemove = false;
                if (CheckAccount(login, CurUserAccounts, TypeCheckAccount.Login))
                {
                    if (!CheckMoneyBeforeRemove(login, CurUserAccounts, TypeCheckAccount.Login))
                        ResultOfRemove = RemoveFromUsers(CurUsers, login, out NewUsers) && RemoveFromUserAccount(CurUserAccounts, login, out NewUserAccounts);
                    else
                        Console.WriteLine("Some account has money over 10! You cannot remove user!");
                }
                else
                {
                    ResultOfRemove = RemoveFromUsers(CurUsers, login, out NewUsers);
                }

                if (ResultOfRemove)
                    Console.WriteLine("The current user was removed!");
            }
            else
            {
                Console.WriteLine("You set an unknown user!");
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowAdminMenu(NewUserAccounts, NewUsers);
        }

        /// <summary>
        /// Удаление аккаунта(счета)
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void RemoveAccount(string[,] CurUserAccounts, string[,] CurUsers)
        {
            Console.Clear();
            Console.WriteLine("---------Removing user's account---------");
            Console.WriteLine("Write a name user:");
            string login = Console.ReadLine();

            string[,] NewUsers = CurUsers;
            string[,] NewUserAccounts = CurUserAccounts;

            if (CheckUser(login, CurUsers))
            {
                Console.WriteLine("Write number of user's account");
                string AccountUser = Console.ReadLine();

                if (CheckAccount(AccountUser, CurUserAccounts, TypeCheckAccount.Account))
                {
                    if (!CheckMoneyBeforeRemove(AccountUser, CurUserAccounts, TypeCheckAccount.Account))
                    {
                        bool ResultOfRemove = RemoveFromUserAccount(CurUserAccounts, login, out NewUserAccounts, AccountUser);
                        if (ResultOfRemove)
                            Console.WriteLine("The current account was removed!");
                    }
                    else
                    {
                        Console.WriteLine("Current account has money over 10! You cannot remove user!");
                    }
                }
                else
                {
                    Console.WriteLine("You set an unknown account!");
                }
            }
            else
            {
                Console.WriteLine("You set an unknown user!");
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowAdminMenu(NewUserAccounts, NewUsers);
        }

        /// <summary>
        /// Удаление пользователя из исходного хранилища
        /// </summary>
        /// <param name="CurUsers"></param>
        /// <param name="UserName"></param>
        /// <param name="NewCurUser"></param>
        /// <returns></returns>
        static bool RemoveFromUsers(string[,] CurUsers, string UserName, out string[,] NewCurUser)
        {
            bool Result = false;
            string[,] TempNewCurUser = CurUsers.GetLength(0) > 1 ? new string[CurUsers.GetLength(0) - 1, 3] : new string[,] { };


            if (TempNewCurUser.GetLength(0) > 0)
            {
                int index = 0;
                for (int i = 0; i < CurUsers.GetLength(0); i++)
                {
                    if (CurUsers[i, 0] != UserName)
                    {
                        for (int j = 0; j < CurUsers.GetLength(1); j++)
                        {
                            TempNewCurUser[index, j] = CurUsers[i, j];
                        }

                        index++;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            Result = TempNewCurUser.GetLength(0) != CurUsers.GetLength(0);
            NewCurUser = TempNewCurUser;
            return Result;
        }

        /// <summary>
        /// Удаление пользовательского аккаунта(счета) из исходного хранилища
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="UserName"></param>
        /// <param name="NewCurUserAccounts"></param>
        /// <param name="UserAccount"></param>
        /// <returns></returns>
        static bool RemoveFromUserAccount(string[,] CurUserAccounts, string UserName, out string[,] NewCurUserAccounts, string UserAccount = "")
        {
            bool Result = false;
            string[,] TempNewCurUser = CurUserAccounts.GetLength(0) > 1 ? new string[CurUserAccounts.GetLength(0) - 1, 4] : new string[,] { };

            if (TempNewCurUser.GetLength(0) > 0)
            {
                int index = 0;
                for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
                {
                    if (CurUserAccounts[i, 0] == UserName && UserAccount == "")
                    {
                        continue;
                    }
                    else if (CurUserAccounts[i, 0] == UserName && CurUserAccounts[i, 1] == UserAccount && UserAccount != "")
                    {
                        continue;
                    }
                    else
                    {
                        for (int j = 0; j < CurUserAccounts.GetLength(1)-1; j++)
                        {
                            TempNewCurUser[index, j] = CurUserAccounts[i, j];
                        }

                        index++;
                    }
                }
            }

            Result = TempNewCurUser.GetLength(0) != CurUserAccounts.GetLength(0);
            NewCurUserAccounts = TempNewCurUser;
            return Result;
        }

        /// <summary>
        /// Смена пользователя
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        static void LogOut(string[,] CurUserAccounts, string[,] CurUsers)
        {
            EnterBank(CurUserAccounts, CurUsers);
        }

        /// <summary>
        /// Отображение списка аккаунтов пользователя
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        /// <param name="UserName"></param>
        static void ShowCurrentUserAccount(string[,] CurUserAccounts, string[,] CurUsers, string UserName)
        {
            Console.Clear();
            Console.WriteLine("----------Account information----------");
            for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
            {
                if (UserName == CurUserAccounts[i, 0])
                {
                    Console.WriteLine("{0} - {1} - Total money:{2}", CurUserAccounts[i, 0],
                                                                     CurUserAccounts[i, 1],
                                                                     CurUserAccounts[i, 2]);                    
                }
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowUserMenu(CurUserAccounts, CurUsers, UserName);
        }

        /// <summary>
        /// Внесение денег на счет
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        /// <param name="UserName"></param>
        static void AddMoney(string[,] CurUserAccounts, string[,] CurUsers, string UserName)
        {
            Console.Clear();
            Console.WriteLine("----------Adding money----------");

            string[,] NewUserAccounts = CurUserAccounts;
            Console.WriteLine("Write an account which you want to use:");
            string UserAccount = Console.ReadLine();

            if (CheckAccount(UserAccount, CurUserAccounts, TypeCheckAccount.Account))
            {
                Console.WriteLine("Write an amount of money which you want to add:");
                string Money = Console.ReadLine();

                if (CheckMoneyValues(Money))
                {
                    NewUserAccounts = ChangeAccountState(CurUserAccounts, UserAccount, Money, TypeMoneyOperation.Adding);
                    Console.WriteLine("You've added money successfully");
                }
                else
                {
                    Console.WriteLine("The value of money must be a number!");
                }
            }
            else
            {
                Console.WriteLine("You set an unknown account!");
            }

            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowUserMenu(NewUserAccounts, CurUsers, UserName);
        }

        /// <summary>
        /// Снятие денег со счета
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        /// <param name="UserName"></param>
        static void TakeMoney(string[,] CurUserAccounts, string[,] CurUsers, string UserName)
        {
            Console.Clear();
            Console.WriteLine("----------Taking money----------");

            Console.WriteLine("Write an account which you want to use:");
            string UserAccount = Console.ReadLine();

            if (CheckAccount(UserAccount, CurUserAccounts, TypeCheckAccount.Account))
            {

                Console.WriteLine("Write an amount of money which you want to take:");
                string Money = Console.ReadLine();

                if (CheckMoneyValues(Money))
                {
                    CurUserAccounts = ChangeAccountState(CurUserAccounts, UserAccount, Money, TypeMoneyOperation.Removing);
                    Console.WriteLine("You've taken money successfully");
                }
                else
                {
                    Console.WriteLine("The value of money must be a number!");
                }
            }
            else
            {
                Console.WriteLine("You set an unknown account!");
            }


            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowUserMenu(CurUserAccounts, CurUsers, UserName);
        }

        /// <summary>
        /// Определение корректности ввода суммы денег
        /// </summary>
        /// <param name="Val"></param>
        /// <returns></returns>
        static bool CheckMoneyValues(string Val)
        {
            bool Result = false;
            foreach (char CharVal in Val)
            {
                switch (CharVal)
                {
                    case '0': Result = true; break;
                    case '1': Result = true; break;
                    case '2': Result = true; break;
                    case '3': Result = true; break;
                    case '4': Result = true; break;
                    case '5': Result = true; break;
                    case '6': Result = true; break;
                    case '7': Result = true; break;
                    case '8': Result = true; break;
                    case '9': Result = true; break;
                }

                if (!Result)
                    break;
            }

            return Result;
        }

        /// <summary>
        /// Пеервод денег с одного счета на другой
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="CurUsers"></param>
        /// <param name="UserName"></param>
        static void TransferMoney(string[,] CurUserAccounts, string[,] CurUsers, string UserName)
        {
            Console.Clear();
            Console.WriteLine("----------Transfering money----------");

            Console.WriteLine("Write a source account:");
            string SourceAccount = Console.ReadLine();

            string[,] NewUserAccounts = CurUserAccounts;
            if (CheckAccount(SourceAccount, CurUserAccounts, TypeCheckAccount.Account))
            {

                Console.WriteLine("Write an target account:");
                string TargetAccount = Console.ReadLine();
                if (CheckAccount(TargetAccount, CurUserAccounts, TypeCheckAccount.Account))
                {
                    Console.WriteLine("Write an amount of money which you want to transfer:");
                    string Money = Console.ReadLine();

                    if (CheckMoneyValues(Money))
                    {
                        NewUserAccounts = ChangeAccountState(NewUserAccounts, SourceAccount, Money, TypeMoneyOperation.Removing);
                        NewUserAccounts = ChangeAccountState(NewUserAccounts, TargetAccount, Money, TypeMoneyOperation.Adding);
                        Console.WriteLine("You've transfered money successfully");
                    }
                    else
                    {
                        Console.WriteLine("The value of money must be a number!");
                    }
                }
                else
                {
                    Console.WriteLine("The target account wasn't found!");
                }
            }
            else
            {
                Console.WriteLine("The source account wasn't found!");
            }


            Console.WriteLine("\nBack to main menu - Press '0'");
            Console.WriteLine("Exit              - Press any key");
            string answer = Console.ReadLine();
            if (answer == "0")
                ShowUserMenu(NewUserAccounts, CurUsers, UserName);

        }

        /// <summary>
        /// Выполнение операций пополнения/снятия денег 
        /// </summary>
        /// <param name="CurUserAccounts"></param>
        /// <param name="UserAccount"></param>
        /// <param name="MoneyValue"></param>
        /// <param name="TypeOperation"></param>
        /// <returns></returns>
        static string[,] ChangeAccountState(string[,] CurUserAccounts, string UserAccount, string MoneyValue, TypeMoneyOperation TypeOperation)
        {
            for (int i = 0; i < CurUserAccounts.GetLength(0); i++)
            {
                if (CurUserAccounts[i, 1] == UserAccount)
                {
                    if (TypeOperation == TypeMoneyOperation.Adding)
                    {
                        CurUserAccounts[i, 2] = (int.Parse(CurUserAccounts[i, 2]) + int.Parse(MoneyValue)).ToString();
                        CurUserAccounts[i, 3] += "+ " + MoneyValue + "; ";
                    }
                    else
                    {
                        CurUserAccounts[i, 2] = (int.Parse(CurUserAccounts[i, 2]) - int.Parse(MoneyValue)).ToString();
                        CurUserAccounts[i, 3] += "- " + MoneyValue + "; ";
                    }

                    break;
                }
            }

            return CurUserAccounts;
        }

    }
}
