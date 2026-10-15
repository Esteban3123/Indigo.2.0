CREATE TABLE [dbo].[HCTIPCONS] (
    [ID]          INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NULL,
    [TIPOCONSU]   TINYINT                                                                          NOT NULL,
    [FECHAREG]    DATETIME                                                                         NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [AGASICITAID] INT                                                                              NULL,
    [REGCONF]     BIT                                                                              CONSTRAINT [DF_HCTIPCONS_REGCONF] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_HCTIPCONS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCTIPCONS_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCTIPCONS_AGASICITA] FOREIGN KEY ([AGASICITAID]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_HCTIPCONS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCTIPCONS_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCTIPCONS] NOCHECK CONSTRAINT [FK_HCTIPCONS_ADINGRESO];


GO
ALTER TABLE [dbo].[HCTIPCONS] NOCHECK CONSTRAINT [FK_HCTIPCONS_AGASICITA];


GO
ALTER TABLE [dbo].[HCTIPCONS] NOCHECK CONSTRAINT [FK_HCTIPCONS_INPACIENT];


GO
ALTER TABLE [dbo].[HCTIPCONS] NOCHECK CONSTRAINT [FK_HCTIPCONS_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IX_HCTIPCONS__IPCODPACI]
    ON [dbo].[HCTIPCONS]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de consulta que realiza el paciente:  1-> Planificación Familiar Primera vez(Habilitar si la edad del paciente es >= a 10 y < a 60 años y si no tiene registro en la tabla de consultas de este mismo tipo)    2->Control Prenatal Primera Vez (Habilitar si la paciente está en estado de embarazo y si la edad del paciente es >= a 10 y < a 60 años. si tiene registro en la tabla de consultas de este mismo tipo con fecha menor a 10 meses no se le muestra la opción)    3-> Control Prenatal (No Primera vez) (Habilitar si la paciente está en estado de embarazo y si la edad del paciente es >= a 10 y < a 60 años. Excluye control prenatal primera vez)    4-> Consulta de Crecimiento y Desarrollo Primera vez (Habilitar si la edad del paciente es < a 10 años y si no tiene registro en la tabla de consultas de este mismo tipo.)    5-> Consulta de Joven Primera vez (Habilitar si la edad del paciente es >= a 10 y < a 30 años y si no tiene registro en la tabla de consultas de este mismo tipo.)    6-> Consulta de Adulto Primera vez (Habilitar si la edad del paciente es >= a 45 años y si no tiene registro en la tabla de consultas de este mismo tipo.)    7.  Consulta por Oftalmología:  8. Consulta de Nutrición:  9.  Consulta de Psicología  10. No Aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPCONS', @level2type = N'COLUMN', @level2name = N'TIPOCONSU';

