#include <iostream>
#include <string>

bool findAndRemove(std::string& str, const std::string& to_remove) {

    size_t pos = str.find(to_remove);

    if (pos == std::string::npos) {
        return false;
    }

    str.erase(pos, to_remove.length());
    return true;
}

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
    else {
        while (true) {
            bool isFind = false;
            if (findAndRemove(str, "{}"))  isFind = true; 
            if (findAndRemove(str, "()"))  isFind = true;
            if (findAndRemove(str, "[]"))  isFind = true;
            
            if (str.empty())        std::cout << "Вс1 хорошо\n"; break;

            if (isFind == false)    std::cout << "Вс1 плохо\n";  break;
        }
    }
}