#include <iostream>
#include <string>
#include <stack>

bool isBracketSequenceValid(const std::string& str) {
    std::stack<char> s;
    for (char ch : str) {
        if (ch == '{' || ch == '(' || ch == '[') {
            s.push(ch);
        }
        else if (ch == '}' || ch == ')' || ch == ']') {

            if (s.empty()) return false;

            char top = s.top();

            if ((top == '{' && ch == '}') ||
                (top == '(' && ch == ')') ||
                (top == '[' && ch == ']')) {
                s.pop();
            }
            else {
                return false;
            }
        }
    }
    return s.empty();
}

int main()
{
    std::setlocale(LC_ALL, "Russian");
    std::string allowed = "{}()[]";
    std::string str;

    std::cout << "Введите строку из {}()[]\n";
    std::getline(std::cin, str);

    if (str.find_first_not_of(allowed) != std::string::npos) 
    {
        std::cout << "Неразрешённые символы\n";
    }
    else if (isBracketSequenceValid(str))
    {
        std::cout << "Всё хорошо\n";
        return 0;
    }
    std::cout << "Всё плохо\n";
    return 0;
}