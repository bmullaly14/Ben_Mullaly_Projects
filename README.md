# Ben_Mullaly_Projects
Public repository of my personal projects.

Agave Atlas - TE Final Capstone
    Agile team of 5 using Scrum methodology created a dynamic and responsive full stack web application in 10 days which allows users to find, review, and share margaritas and the restaurants that serve them. Features include SMTP email invitations via MailKit NuGet package, Twitter API integration, "Patio Weather Tracker" which consumes the NOAA/National Weather Service API. Authenticated users can add a restaurant via Yelp API integration, review restaurants, and add drinks to restaurants. Authenticated Admin can delete restaurants, drinks, and reviews. Technologies include: Vue.js front end interacting with REST API server built in C# with .NET CORE using MVC and DAO design patterns to manage the SQL database and Yelp, NOAA, ZipCode, Outlook SMTP, and Twitter APIs.

Module 1 Mini Capstone - Vending Machine
    Group of 4 produced a console line program emulating a virtual vending machine. I added a "ta-da!" sound which is played when "toys" are dispensed from the machine.
    **Sounds only play on Windows machines due to use of System.Media SoundPlayer base class.

Module 2 Mini Capstone - TEnmo
    Group of 4 produced a server and partial console client UI emulating TEnmo, a money transfer app. Utilizing C#, MS SQL, JSON, PostMan, integration testing, and JWT Authentication developed a RESTful API server and basic command line application that registers new users, requires login authentication, and permits and validates balance transfers. 

Card Dealer
    C# console line program that "deals" (prints to console) cards for War and Euchre. Uses OOP principles with "Card" and "Player" objects.

Ping Pong Tournament Tool
    C# console line program that allows user to create and run ping pong tournaments for singles and doubles using an "IPlayable" interface in Single Elimination, Double Elimination, and Round Robin style brackets using the Bracket parent class.