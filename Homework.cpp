#include <iostream>
#include <string>

int main()
{
    std::string allowed = "{}()[]";
    std::string str;

    std::cout << "Введите строку из {}()[]\n";
    std::getline(std::cin, str);
    if (str.find_first_not_of(allowed) != std::string::npos) 
    {
        std::cout << "Неразрешённые символы\n";
    }
    else if (true) 
    {
        std::cout << "Строка не существует\n";
    }
    else
    {
        std::cout << "Строка существует\n";
    }
}