## Main first steps

1. Create *Asp.net core webApi* project 
2. Download packages of EF Core, tools, design and sql provider
3. Add *Data* folder that has folder for models and another for the *DbContext*
    - you may add seeding, to add some data on creating

- In the 1:M relation, add int for foreign id and a nullable navigational property which indicates that loading this property is optional for the M side. And add *ICollection* of otherModel for the 1 side.
    - That doesn't meat that the relation is optional itself, if we need the relation to be optional we make the foreign ke nullable

- In M:M relation, we put *ICollection* of otherModel in both classes. 
    - You may initialize it as new *HashSet*

4. Fill the context file and add the connectionString
5. Add-migration and update-database
6. Divide you code into regions or tiers, Data Access Layer, Business Logic Layer and Presentational Layer
    - Add the data folder in DAL
    - Consider current project as persentatioal layer, add new two projects of type *ClassLibirary* to represent the other two layers
    - Add a reference of each layer to the above one

- Your context in DAL, but the configuration in PL
    - So You need to run migration in DAL and make sure to set PL as the startup project
