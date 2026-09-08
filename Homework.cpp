#include <iostream>
#include <string>
#include <vector>
#include <stack>
#include <cctype>
#include <stdexcept>
#include <sstream>

using namespace std;

int getPriority(char op) {
    if (op == '+' || op == '-') return 1;
    if (op == '*' || op == '/') return 2;
    return 0;
}

double applyOp(double a, double b, char op) {
    switch (op) {
    case '+': return a + b;
    case '-': return a - b;
    case '*': return a * b;
    case '/':
        if (b == 0) throw runtime_error("Деление на ноль!");
        return a / b;
    default: throw runtime_error("Неверный оператор");
    }
}


double calculation(const string& tokens) {
    stack<double> values;
    stack<char> ops;

    for (size_t i = 0; i < tokens.length(); i++) {
        if (isspace(tokens[i])) continue;

        if (isdigit(tokens[i]) || tokens[i] == '.') {
            string valStr = "";
            while (i < tokens.length() && (isdigit(tokens[i]) || tokens[i] == '.')) {
                valStr += tokens[i++];
            }
            values.push(stod(valStr));
            //В этот момент индекс указывает на символ операции
            i--;
        }
        else if (tokens[i] == '(') {
            ops.push(tokens[i]);
        }
        else if (tokens[i] == ')') {
            while (!ops.empty() && ops.top() != '(') {
                if (values.size() < 2) throw runtime_error("Неверный формат выражения");
                double val2 = values.top(); values.pop();
                double val1 = values.top(); values.pop();
                char     op =    ops.top();    ops.pop();

                values.push(applyOp(val1, val2, op));
            }
            if (!ops.empty()) ops.pop();
            else throw runtime_error("Пропущена открывающая скобка");
        }
        else if (tokens[i] == '+' || tokens[i] == '-' || tokens[i] == '*' || tokens[i] == '/') {
            while (!ops.empty() && getPriority(ops.top()) >= getPriority(tokens[i])) {
                if (values.size() < 2) throw runtime_error("Неверный формат выражения");
                double val2 = values.top(); values.pop();
                double val1 = values.top(); values.pop();
                char op = ops.top(); ops.pop();

                values.push(applyOp(val1, val2, op));
            }
            ops.push(tokens[i]);
        }
        else {
            throw runtime_error("Недопустимый символ в выражении");
        }
    }

    while (!ops.empty()) {
        if (values.size() < 2) throw runtime_error("Неверный формат выражения");
        double val2 = values.top(); values.pop();
        double val1 = values.top(); values.pop();
        char   op   =    ops.top();    ops.pop();

        values.push(applyOp(val1, val2, op));
    }

    if (values.size() != 1) throw runtime_error("Ошибка при парсинге выражения");
    return values.top();
}

int main() {
    setlocale(LC_ALL, "Russian");

    string expression;
    std::cin >> expression;

    cout << "Выражение: " << expression << endl;

    try {
        double result = calculation(expression);
        cout << "Результат: " << result << endl;
    }
    catch (const exception& e) {
        cerr << "Ошибка: " << e.what() << endl;
    }

    return 0;
}
