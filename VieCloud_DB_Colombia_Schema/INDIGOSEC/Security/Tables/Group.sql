CREATE TABLE [Security].[Group] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Code]         VARCHAR (3)  NOT NULL,
    [Description]  VARCHAR (60) NOT NULL,
    [TimeStamp]    ROWVERSION   NOT NULL,
    [State]        BIT          NOT NULL,
    [Synchronized] VARCHAR (1)  NOT NULL,
    [GroupType]    TINYINT      NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_SEGGRUUSU_CODGRUPOU] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de grupo: 1:Global, 2:Por tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Group', @level2type = N'COLUMN', @level2name = N'GroupType';

