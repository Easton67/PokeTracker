/* check to see if the database exists, if so, drop it*/
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'caughtornotdb')
BEGIN
	DROP DATABASE caughtornotdb
	print '' print '*** dropping database caughtornotdb ***'
END
GO

print '' print '*** creating database caughtornotdb ***'
GO
CREATE DATABASE [caughtornotdb]
GO

print '' print '*** using database caughtornotdb ***'
GO
USE [caughtornotdb]
GO

/* User Table */
print '' print '*** creating User Table ***'
GO
CREATE TABLE [dbo].[User] (
	[UserID] 		  [int] IDENTITY(100000,1)   NOT NULL,
	[ProfileName] 	  [nvarchar](100)          	 NOT NULL,
	[Email]			  [nvarchar](100)			 NOT NULL,
	[FirstName] 	  [nvarchar](50)             DEFAULT 'Unknown',
	[LastName] 		  [nvarchar](50)             DEFAULT 'Unknown',
	[PasswordHash]	  [nvarchar](100)		     NOT NULL DEFAULT
	'5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8',
	[ImageFilePath]   [nvarchar](500)            NULL DEFAULT 
	'defaultAccount.png',
	[Active]		  [bit]						 NOT NULL DEFAULT 1,
	CONSTRAINT [pk_UserID] PRIMARY KEY ([UserID]),
	CONSTRAINT [ak_ProfileName] UNIQUE([ProfileName]),
	CONSTRAINT [ak_Email] UNIQUE([Email])
)
GO

/* Pokemon Table */
print '' print '*** creating Pokemon Table ***'
GO
CREATE TABLE [dbo].[Pokemon] (
	[PokemonID]      [int] IDENTITY(100000,1)   NOT NULL,
	[Name] 	  	    [nvarchar](300)            NOT NULL,
    [DexNumber] 	[int]                      NOT NULL,
	[Sprite]        [nvarchar](100)            NOT NULL,
	[CaughtOrNot]   [bit]                      NOT NULL
	CONSTRAINT [pk_PokemonID] PRIMARY KEY ([PokemonID]),
)
GO

/* Evolution Table */
print '' print '*** creating Evolution Table ***'
GO
CREATE TABLE [dbo].[Evolution] (
	[EvolutionID]   [int] IDENTITY(100000,1)   NOT NULL,                    
    [Level]         [int],        
	[Method]        [nvarchar](100),                   
	CONSTRAINT [pk_EvolutionID] PRIMARY KEY ([EvolutionID])
)
GO

/* Game Table */
print '' print '*** creating Game Table ***'
GO
CREATE TABLE [dbo].[Game] (
	[GameID]        [int]  IDENTITY(100000,1)   NOT NULL,  
	[Name] 			[nvarchar](100),                  
    [Generation]    [int],
	CONSTRAINT [pk_GameID] PRIMARY KEY ([GameID])
)
GO

/* Location Table */
print '' print '*** creating Location Table ***'
GO
CREATE TABLE [dbo].[Location] (
	[LocationID]      			[int] IDENTITY(100000,1)   NOT NULL,
	[Name] 	  	      			[nvarchar](300)            NOT NULL,
	[EvolutionLevelOrMethod]	[nvarchar](100)			   NOT NULL,
	[CaughtOrNot]     			[bit]                      NOT NULL,
	[Generation]      			[int]                      NOT NULL,
	CONSTRAINT [pk_LocationID] PRIMARY KEY ([LocationID])
)
GO

/* PokemonLocation */
print '' print '*** creating PokemonLocation table ***'
GO
CREATE TABLE [dbo].[PokemonLocation] (
	[PokemonID]      	[int]						 NOT NULL,
	[LocationID]		[int]						 NOT NULL,
	CONSTRAINT [fk_PokemonLocation_PokemonID] FOREIGN KEY([PokemonID])
		REFERENCES [dbo].[Pokemon]([PokemonID]) ON DELETE CASCADE,
		
	CONSTRAINT [pk_PokemonLocation] PRIMARY KEY([PokemonID], [LocationID])
)
GO

/* PokemonGame */
print '' print '*** creating PokemonGame table ***'
GO
CREATE TABLE [dbo].[PokemonGame] (
	[PokemonID]      	[int]						 NOT NULL,
	[GameID]		    [int]						 NOT NULL,
	CONSTRAINT [fk_PokemonGame_PokemonID] FOREIGN KEY([PokemonID])
		REFERENCES [dbo].[Pokemon]([PokemonID]) ON DELETE CASCADE,
		
	CONSTRAINT [pk_PokemonGame] PRIMARY KEY([PokemonID], [GameID])
)
GO



