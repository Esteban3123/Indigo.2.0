CREATE TABLE [dbo].[INCONSECU] (
    [IDCONSECU] CHAR (8)     NOT NULL,
    [CONSECDES] CHAR (80)    NOT NULL,
    [CONNUMACT] DECIMAL (18) NOT NULL,
    CONSTRAINT [PK_INCONSECU] PRIMARY KEY CLUSTERED ([IDCONSECU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero consecutivo actual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONNUMACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descricion del consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'CONSECDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECU', @level2type = N'COLUMN', @level2name = N'IDCONSECU';

