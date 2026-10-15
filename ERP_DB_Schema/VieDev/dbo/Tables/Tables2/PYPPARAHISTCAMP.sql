CREATE TABLE [dbo].[PYPPARAHISTCAMP] (
    [ID]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPYPPARAHISTO]    INT NOT NULL,
    [IDPYPPARAHISTCAMP] INT NOT NULL,
    [VISIBLE]           BIT NOT NULL,
    [OBLIGA]            BIT NOT NULL,
    CONSTRAINT [PK_PYPPARAHISTOD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de la Historia parametrizada campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PYPPARAHISTCAMP', @level2type = N'COLUMN', @level2name = N'IDPYPPARAHISTCAMP';

