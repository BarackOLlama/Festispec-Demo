
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2018 16:46:46
-- Generated from EDMX file: C:\Users\Evert\source\repos\Avans-Prog5\prog5-kwisspel-1819-bart-evert\Kwisspel\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [kwis];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_answers_answers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[answers] DROP CONSTRAINT [FK_answers_answers];
GO
IF OBJECT_ID(N'[dbo].[FK_answers_questions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[answers] DROP CONSTRAINT [FK_answers_questions];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[answers];
GO
IF OBJECT_ID(N'[dbo].[category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[category];
GO
IF OBJECT_ID(N'[dbo].[questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[questions];
GO
IF OBJECT_ID(N'[kwisModelStoreContainer].[database_firewall_rules]', 'U') IS NOT NULL
    DROP TABLE [kwisModelStoreContainer].[database_firewall_rules];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'answers'
CREATE TABLE [dbo].[answers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [content] nvarchar(max)  NOT NULL,
    [questionid] int  NOT NULL,
    [categoryid] int  NOT NULL
);
GO

-- Creating table 'questions'
CREATE TABLE [dbo].[questions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [content] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'categories'
CREATE TABLE [dbo].[categories] (
    [ID] int  NOT NULL,
    [naam] nchar(10)  NOT NULL
);
GO

-- Creating table 'database_firewall_rules'
CREATE TABLE [dbo].[database_firewall_rules] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(128)  NOT NULL,
    [start_ip_address] varchar(45)  NOT NULL,
    [end_ip_address] varchar(45)  NOT NULL,
    [create_date] datetime  NOT NULL,
    [modify_date] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'answers'
ALTER TABLE [dbo].[answers]
ADD CONSTRAINT [PK_answers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'questions'
ALTER TABLE [dbo].[questions]
ADD CONSTRAINT [PK_questions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'categories'
ALTER TABLE [dbo].[categories]
ADD CONSTRAINT [PK_categories]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] in table 'database_firewall_rules'
ALTER TABLE [dbo].[database_firewall_rules]
ADD CONSTRAINT [PK_database_firewall_rules]
    PRIMARY KEY CLUSTERED ([id], [name], [start_ip_address], [end_ip_address], [create_date], [modify_date] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [questionid] in table 'answers'
ALTER TABLE [dbo].[answers]
ADD CONSTRAINT [FK_answers_questions]
    FOREIGN KEY ([questionid])
    REFERENCES [dbo].[questions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_answers_questions'
CREATE INDEX [IX_FK_answers_questions]
ON [dbo].[answers]
    ([questionid]);
GO

-- Creating foreign key on [categoryid] in table 'answers'
ALTER TABLE [dbo].[answers]
ADD CONSTRAINT [FK_answers_answers]
    FOREIGN KEY ([categoryid])
    REFERENCES [dbo].[categories]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_answers_answers'
CREATE INDEX [IX_FK_answers_answers]
ON [dbo].[answers]
    ([categoryid]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------