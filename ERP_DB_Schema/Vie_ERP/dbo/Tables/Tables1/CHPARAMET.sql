CREATE TABLE [dbo].[CHPARAMET] (
    [CODCENATE]                                CHAR (10)    NOT NULL,
    [REQAPRSOL]                                BIT          NOT NULL,
    [PERSOLRES]                                BIT          NOT NULL,
    [HORMINSOL]                                TINYINT      NOT NULL,
    [PERHOSSIN]                                BIT          NOT NULL,
    [NUMDIAMAX]                                TINYINT      NOT NULL,
    [NUMHORMIN]                                TINYINT      NOT NULL,
    [FECPARINI]                                DATETIME     NOT NULL,
    [TIEMTABLE]                                INT          NOT NULL,
    [HORRESALE]                                TINYINT      NULL,
    [EXICOINFC]                                BIT          NOT NULL,
    [INDAUDFOR]                                NUMERIC (18) NOT NULL,
    [ACTINTERF]                                CHAR (1)     NOT NULL,
    [GENHORACORTE]                             TIME (0)     CONSTRAINT [DF__CHPARAMET__GENHO__16A44564] DEFAULT ('23:59:59') NOT NULL,
    [DASHLIMPIEZA]                             BIT          NULL,
    [BreakfastStartTime]                       TIME (0)     NULL,
    [BreakfastEndTime]                         TIME (0)     NULL,
    [LunchStartTime]                           TIME (0)     NULL,
    [LunchEndTime]                             TIME (0)     NULL,
    [DinnerStartTime]                          TIME (0)     NULL,
    [DinnerEndTime]                            TIME (0)     NULL,
    [MorningSupplementStartTime]               TIME (0)     NULL,
    [MorningSupplementEndTime]                 TIME (0)     NULL,
    [AfternoonSupplementStartTime]             TIME (0)     NULL,
    [AfternoonSupplementEndTime]               TIME (0)     NULL,
    [OtherMealStartTime]                       TIME (0)     NULL,
    [OtherMealEndTime]                         TIME (0)     NULL,
    [SaveDischargeWithAllLegalizedMedications] BIT          CONSTRAINT [DF__CHPARAMET__SaveD__45D06EB0] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CHPARAMET] PRIMARY KEY CLUSTERED ([CODCENATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guardar Descarga ConTodos LosMedicamentos Legalizados  1 = true    o = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'SaveDischargeWithAllLegalizedMedications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Final de otra Comida ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial de otra Comida ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'OtherMealStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Final suplemento en la tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial de la Comida de Suplemento de la tarde', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'AfternoonSupplementStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Final  suplemento en la mañana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial de la Comida de Suplemento de la mañana ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'MorningSupplementStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Fianl de la  cena', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicio cena', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DinnerStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora final almuerzo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de Inicio del almuerzo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'LunchStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ultima hora del desayuno.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastEndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Inicial del desayuno  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'BreakfastStartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para indicar si se va implementar el DashBoard de limpieza y desinfección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'DASHLIMPIEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aqui va la hora de corte para la liquidación de estancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'GENHORACORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se activa la interfaz de homologacion de camas para validar en hospitalizacion   0: No  1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'ACTINTERF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '01-06-2024 Apartir de la fecha, este campo se marca obsoleto por el PBI: 17646 en Azure Devops.       Exigir Consentimiento Informado Cirugia   1:Si;  0:No;    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'EXICOINFC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Horas Minimas Para Disparar Alertas de las Reservas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORRESALE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo en segundos para rotacion de la informacion en el Tablero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'TIEMTABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Arranque del Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'FECPARINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de horas minimo para confirmar Reservas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMHORMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Dias maximo para asignar Reservas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'NUMDIAMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite dejar a pacientes en observacion sin asignar camillas, solo aplica a la Unidad de Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERHOSSIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece el numero de horas minima para solicitar dietas, aplica solo a pacientes en Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'HORMINSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite realizar solicitudes de dietas de camas reservadas confirmadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'PERSOLRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Requiere aprobacion para emitir las solicitudes de dieta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'REQAPRSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centron de Atencion en donde Ingresa el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMET', @level2type = N'COLUMN', @level2name = N'CODCENATE';

