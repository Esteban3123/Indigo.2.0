CREATE TABLE [dbo].[ADPARAMET] (
    [CODCENATE]                 CHAR (10)    NOT NULL,
    [TRINUMCLA]                 INT          NULL,
    [TRINUMTR1]                 INT          NULL,
    [TRINUMTR2]                 INT          NULL,
    [TRINUMTR3]                 INT          NULL,
    [TRINUMTR4]                 INT          NULL,
    [VIGMESING]                 INT          NULL,
    [INGAUTOMA]                 BIT          NULL,
    [INGHOSPIT]                 BIT          NULL,
    [PERMODTRI]                 BIT          NULL,
    [SALARIMIN]                 MONEY        NULL,
    [INDAUDFOR]                 NUMERIC (18) NOT NULL,
    [SERVACINTE]                CHAR (1)     NULL,
    [NUMHORATE]                 INT          NULL,
    [TRINUMTR5]                 INT          NULL,
    [SERRASANTI]                CHAR (20)    NULL,
    [NUMHORHOS]                 INT          NULL,
    [IDDESCRIPCIONRELACIONADA]  INT          NULL,
    [CREARPACIENTEDATOSBASICOS] BIT          NULL,
    [AllowPreTriage]            BIT          NULL,
    CONSTRAINT [PK_ADPARAMET] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_ADPARAMET_INCUPSIPS] FOREIGN KEY ([SERRASANTI]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[ADPARAMET] NOCHECK CONSTRAINT [FK_ADPARAMET_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir creación del Pre Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'AllowPreTriage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir crear paciente triage con datos básicos:   1 - SI   2 - NO  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CREARPACIENTEDATOSBASICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parámetro para establecer el numero de horas para un reigreso en Hospitalización, el parámetro esta en Parámetros de Admisiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio IPS parametrizado para Rastreo de anticuerpos en modulo de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERRASANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de minutos maximo para la atencion inicial en TRIAGE 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parámetro para establecer el numero de horas para un reigreso en Urgencia, el parámetro debe estar en Parámetros de Admisiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Carga citas desde Consulta Externa (0- Ninguna 2- Nativo 1 - Externo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SERVACINTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Salario Minimo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'SALARIMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Omitir Dx Sindromatico y Permitir Modificar Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'PERMODTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Ingresos Hospitalarios:  True = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGHOSPIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Crear Ingreso Automatico en Triage:  True = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'INGAUTOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vigencia de Ingresos en meses. Esta opcion no permite abrir ingresos de una fecha inferior a la fecha actual menos los dias establecidos en el control', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'VIGMESING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de minutos maximo para la atencion inicial en TRIAGE 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de minutos maximo para la atencion inicial en TRIAGE 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de minutos maximo para la atencion inicial en TRIAGE 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de minutos maximo para la atencion inicial en TRIAGE 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMTR1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de minutos Maximo para la Clasificacion de TRIAGE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'TRINUMCLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';

