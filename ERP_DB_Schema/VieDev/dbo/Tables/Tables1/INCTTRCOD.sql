CREATE TABLE [dbo].[INCTTRCOD] (
    [CONSECUTI] INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACANTERI] CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Indentification_Ofuscado", 0)') NOT NULL,
    [FECREGIST] DATETIME                                                                       NOT NULL,
    [ESTADOPAC] CHAR (1)                                                                       NOT NULL,
    [PACINUEVO] CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [CODUSUARI] CHAR (20)                                                                      NOT NULL,
    [TRASPACYA] BIT                                                                            NULL,
    [OBSERVACI] VARCHAR (250)                                                                  NULL,
    CONSTRAINT [PK_INCTTRCOD] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Activo  2-Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCTTRCOD', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';

