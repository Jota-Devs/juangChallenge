CREATE TABLE [dbo].[TagItems] (
    [Id]    INT            IDENTITY (1, 1) NOT NULL,
    [Tag]   NVARCHAR (MAX) NULL,
    [Count] INT            NOT NULL,
    CONSTRAINT [PK_dbo.TagItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

