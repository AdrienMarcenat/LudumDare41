# LudumDare41
Authors : @AdrienMarcenat @Swynfel

# Coding standard

* { always on newline
* Member are PascalCased prefixed by "m_"
* Class and Function name are PascalCased
* All other variables are camelCased

# Coding best practices

Avoid decoupling, to that extent use the C# event (Observer pattern).
In particular, the code should compile if all UI is removed.