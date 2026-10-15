CREATE TABLE [dbo].[HCESCALDRETE] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGSIS]    DATETIME                                                                         NOT NULL,
    [ACTIVIDAD]    TINYINT                                                                          NOT NULL,
    [RESPIRACION]  TINYINT                                                                          NOT NULL,
    [CIRCULACION]  TINYINT                                                                          NOT NULL,
    [NIVCONSCIENC] TINYINT                                                                          NOT NULL,
    [SATURAOXIG]   TINYINT                                                                          NOT NULL,
    [RESULTADO]    INT                                                                              NOT NULL,
    CONSTRAINT [PK_HCESCALDRETE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALDRETE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCESCALDRETE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCESCALDRETE] NOCHECK CONSTRAINT [FK_HCESCALDRETE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCESCALDRETE] NOCHECK CONSTRAINT [FK_HCESCALDRETE_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saturación de Oxigeno 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'SATURAOXIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Consciencia 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'NIVCONSCIENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CIRCULACION 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'CIRCULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESPIRACION 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'RESPIRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Actividad: Capaz de moverse voluntariamente o seguir órdenes 2-1-0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'ACTIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Profesional de la salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALDRETE', @level2type = N'COLUMN', @level2name = N'ID';

