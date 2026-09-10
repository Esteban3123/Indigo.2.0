'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class MedicinesProductionRepository
    Inherits GenericRepository(Of MedicinesProduction)
    Implements IMedicinesProductionRepository, Inject

    ''' <summary>
    ''' Contexto de Package
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Package
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetMedicinesProductionById(id As Integer, Optional tracking As Boolean = True) As MedicinesProduction Implements IMedicinesProductionRepository.GetMedicinesProductionById
        Return (From bg In _context.MedicinesProduction
                Where bg.Id = id
                Select bg).FirstOrDefault()
    End Function
End Class