scafolding cmnd 


dotnet ef dbcontext scaffold   "Host=ep-shiny-king-adn1m8p7-pooler.c-2.us-east-1.aws.neon.tech;Database=SriDurgaHariHaraEnterprices;Username=neondb_owner;Password=npg_Bd6jIwYRmy0Q;Ssl Mode=Require;Trust Server Certificate=true"   Npgsql.EntityFrameworkCore.PostgreSQL   --context AppDbContext   --context-dir Persistence   --output-dir Models   --force   --project .   --startup-project ../Api