CREATE TABLE [Portfolio].[Circular030] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]             INT          NOT NULL,
    [Trimester]        TINYINT      NOT NULL,
    [CreationUser]     VARCHAR (20) CONSTRAINT [DF_Circular030_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME     CONSTRAINT [DF_Circular030_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Circular030__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el trimestre que se va a reportar  1 - Primer Trimestre  2 - Segundo Trimestre  3 - Tercero Trimestre  4 - Cuarto Trimestre', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'Circular030', @level2type = N'COLUMN', @level2name = N'Trimester';

