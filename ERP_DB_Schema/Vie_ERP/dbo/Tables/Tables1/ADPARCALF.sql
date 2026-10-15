CREATE TABLE [dbo].[ADPARCALF] (
    [CODCENATE] CHAR (10)    NOT NULL,
    [CODENTIDA] CHAR (9)     NOT NULL,
    [APLFESTR3] BIT          NOT NULL,
    [APLFESTR4] BIT          NOT NULL,
    [CONHORTRI] INT          NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADCALFEST] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [CODENTIDA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite establecer el parametro con el que se controla los parametros de apertura de Ingresos segun el calendario de no atenciones en TRIAGE III y IV.    1: Control Paciente  2: Registro TRIAGE    Nota: Este parametro tomara la hora segun la seleccion y la comparara contra el calendario para decidir si realiza apertura de ingreso o no.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CONHORTRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica horario a Festivos en TRIAGE 4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica horario a Festivos en TRIAGE 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'APLFESTR3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Entidad EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPARCALF', @level2type = N'COLUMN', @level2name = N'CODCENATE';

