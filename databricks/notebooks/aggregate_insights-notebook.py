pip install pyodbc pandas


Driver={ODBC Driver 17 for SQL Server};Server=tcp:your_server.database.windows.net,1433;Database=your_database;Uid=your_username;Pwd=your_password;Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;


import pyodbc
import pandas as pd
from databricks import sql

# Retrieve the connection string securely from Databricks secret
connection_string = dbutils.secrets.get(scope="azure_sql", key="sql_connection_string")

# Connect to the Azure SQL database
conn = pyodbc.connect(connection_string)

# Query to fetch access logs
query = """
SELECT CAST(AccessTime AS DATE) AS AccessDate, DATEPART(hour, AccessTime) AS AccessHour, COUNT(*) AS CheckIns
FROM AccessLogs
WHERE AccessType = 'CheckIn'
GROUP BY CAST(AccessTime AS DATE), DATEPART(hour, AccessTime)
ORDER BY AccessDate, AccessHour;
"""

# Execute the query and load the results into a pandas DataFrame
df = pd.read_sql(query, conn)

# Close the connection
conn.close()

# Display the first few rows of the DataFrame
display(df.head())

# Analyze the data for peak attendance times
peak_times = df.groupby('AccessHour')['CheckIns'].mean().sort_values(ascending=False)
print("Peak Attendance Hours (Average Check-Ins):")
print(peak_times)
