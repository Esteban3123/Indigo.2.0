CREATE TABLE [dbo].[HCFICHA230] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CONTACASO]           INT           NULL,
    [DOSISAPLI]           INT           NULL,
    [FUENTEINFO]          INT           NULL,
    [FIEBRE]              INT           NULL,
    [AMIGDALITIS]         INT           NULL,
    [FARINGITIS]          INT           NULL,
    [LARINGITIS]          INT           NULL,
    [PRESEMEMB]           INT           NULL,
    [COMPLICA]            INT           NULL,
    [TIPOCOMPLI]          INT           NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECE]           DATE          NULL,
    [MUESTRA]             VARCHAR (50)  NULL,
    [PRUEBA]              VARCHAR (50)  NULL,
    [AGENTE]              VARCHAR (50)  NULL,
    [RESULTADO]           VARCHAR (50)  NULL,
    [FECHARESUL]          DATE          NULL,
    [VALOR]               VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA230] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA230_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA230_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA230] NOCHECK CONSTRAINT [CK_HCFICHA230_JSON];


GO
ALTER TABLE [dbo].[HCFICHA230] NOCHECK CONSTRAINT [FK_HCFICHA230_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON    Desde versión "V01_2020-03-06" -->  - CARNET_VACUNACION: bit (0=No, 1=Si)  - TIPO_VACUNA: int ( 1=DPT, 2=Pentavalente, 3=TD, 4=Otra)  - OTRA_VACUNA: string   - FECHA_ULTIMA_DOSIS: datetime    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (Datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'fecha de resultado (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'resultado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'agent', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '  Fecha de Recepción (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Fecha Toma de Examen (dd-mm-aaaa)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Complicación  1=Neurológica  2=Renal  3=Cardíaca  4=Traquetomía  5=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMPLICA  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'COMPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRESEMEMB  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'PRESEMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'LARINGITIS  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'LARINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Faringitis  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FARINGITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Amigdalitis  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'AMIGDALITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fiebre  1=si2=no3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(campo eliminado desde version V01_2020-03-06)   Fuente de Información de las Dosis :  1=Carné de Vacunación   2=Verbal   3=PAI Web   4=Desconocido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'FUENTEINFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Aplicadas de Vacuna Antidiftérica:   1 = Ninguna   2 = Una   3 = Dos   4 = Tres o más Dosis   ( 5, 6 --> items nuevos desde version V01_2020-03-06 )  5 = Primer Refuerzo   6 = Segundo Refuerzo   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'DOSISAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contacto de un Caso Confirmado  1=si  2=no  3=desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CONTACASO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA230', @level2type = N'COLUMN', @level2name = N'ID';

