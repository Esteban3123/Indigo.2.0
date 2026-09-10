'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Rafael Eduardo Patiño Cabrera
' Created          : 23-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Crystal
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports System.Dynamic
Imports Infrastructure.CrossCutting.Resources
Public Class LevelPatientRepository
    Inherits GenericRepository(Of ADNIVELES)
    Implements ILevelPatientRepository

    'Contexto del repositorio de Indigo Vie Cloud Platform
    Private _crystalContext As ICrystalModelUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="crystalContext">Contexto</param>
    Public Sub New(ByVal crystalContext As ICrystalModelUnitOfWork)
        MyBase.New(crystalContext)
        Me._crystalContext = crystalContext
    End Sub

End Class
