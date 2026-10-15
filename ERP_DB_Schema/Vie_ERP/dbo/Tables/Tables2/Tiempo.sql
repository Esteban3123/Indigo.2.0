CREATE TABLE [dbo].[Tiempo] (
    [Id]           INT          NOT NULL,
    [Fecha]        DATETIME     NOT NULL,
    [Año]          INT          NOT NULL,
    [Mes]          INT          NOT NULL,
    [Dia]          INT          NOT NULL,
    [MesNombre]    VARCHAR (50) NOT NULL,
    [DiaNombre]    VARCHAR (50) NOT NULL,
    [DiaAño]       INT          NOT NULL,
    [SemanaAño]    INT          NOT NULL,
    [Semestre]     INT          NOT NULL,
    [Trimestre]    INT          NOT NULL,
    [Cuatrimestre] INT          NOT NULL,
    [DiaHabil]     BIT          NOT NULL
);

