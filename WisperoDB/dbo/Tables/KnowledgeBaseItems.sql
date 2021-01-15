CREATE TABLE [dbo].[KnowledgeBaseItems] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Query]        NVARCHAR (MAX) NULL,
    [Answer]       NVARCHAR (MAX) NULL,
    [Tags]         NVARCHAR (MAX) NULL,
    [LastUpdateOn] DATETIME       NOT NULL,
    [RowVersion]   ROWVERSION     NOT NULL,
    CONSTRAINT [PK_dbo.KnowledgeBaseItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

