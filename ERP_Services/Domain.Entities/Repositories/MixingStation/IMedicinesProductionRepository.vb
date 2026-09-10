'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IMedicinesProductionRepository
    Inherits IRepository(Of MedicinesProduction)

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetMedicinesProductionById(id As Integer, Optional tracking As Boolean = True) As MedicinesProduction

End Interface
