/*
  Script to setup SQLDB tables

  Version, Date, Author, Comment
  V1, 2021-07-16, Qiniso Mazibuko, V1 

*/

-- ####################################################################################################
--- Create the Members Table
-- ####################################################################################################



CREATE TABLE Members (
    MemberId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Surname NVARCHAR(100),
    QRCode NVARCHAR(255),
    Email NVARCHAR(255)
);

-- ####################################################################################################
---- Create the AccessLogs Table
-- ####################################################################################################


CREATE TABLE AccessLogs (
    AccessLogId INT PRIMARY KEY IDENTITY(1,1),
    MemberId INT,
    AccessTime DATETIME,
    AccessType NVARCHAR(50),
    ClubLocation NVARCHAR(255),
    Synced BIT DEFAULT 0, -- To mark if the log has been synced with the cloud
    CONSTRAINT FK_AccessLogs_Members FOREIGN KEY (MemberId)
        REFERENCES Members(MemberId)
);

-- ####################################################################################################
--- Optional: Index for unsynced logs for efficient querying
-- ####################################################################################################

-- Optional: Index for unsynced logs for efficient querying
CREATE INDEX IDX_AccessLogs_Synced
ON AccessLogs (Synced);
