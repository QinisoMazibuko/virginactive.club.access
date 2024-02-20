# Databricks notebook source
# MAGIC %md
# MAGIC # Analyze member Access_Logs for peak attendance times

# COMMAND ----------

# MAGIC %md
# MAGIC JDBC Connection for azure SQL Server

# COMMAND ----------

import pyodbc
import pandas as pd
from pyspark.sql import SparkSession
from pyspark.sql.functions import format_string

spark = SparkSession.builder.appName("virgin active club access").getOrCreate()

jdbcHostname ="virginactive-clubaccess-server.database.windows.net"
jdbcDatabase ="VirginActiveClubAccess"
jdbcPort = 1433
jdbcUrl = "jdbc:sqlserver://{0}:{1};database={2}".format(jdbcHostname,jdbcPort,jdbcDatabase)

# Using Databricks secrets to retrieve database credentials
dbuser = dbutils.secrets.get(scope="virginactive", key="dbUsername")
dbpassword = dbutils.secrets.get(scope="virginactive", key="dbPassword")


connectionProperties = {
    "user": dbuser,
    "password": dbpassword,
    "driver": "com.microsoft.sqlserver.jdbc.SQLServerDriver"
}

# COMMAND ----------

# MAGIC %md
# MAGIC Execute SQL Query to read data through JDBC Connection and retrive access_logs from source table

# COMMAND ----------

query =   """
    (SELECT CAST(AccessTime AS DATE) AS AccessDate,
            DATEPART(hour, AccessTime) AS AccessHour,
            COUNT(*) AS CheckIns
     FROM AccessLogs
     WHERE AccessType = 'CheckIn'
     GROUP BY CAST(AccessTime AS DATE), DATEPART(hour, AccessTime)
    ) AS peak_times
"""
df = spark.read.jdbc(url=jdbcUrl,table=query, properties=connectionProperties)

# Applying ordering after loading data
df_ordered = df.orderBy("AccessDate", "AccessHour")

df_with_time = df_ordered.withColumn("PeakAccessTime", format_string("%02d:00", "AccessHour"))

# Display the DataFrame
display(df_with_time)
