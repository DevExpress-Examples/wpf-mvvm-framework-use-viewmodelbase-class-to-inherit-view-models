Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations

Namespace Example.ViewModel

    Public Class MainViewModel
        Inherits ViewModelBase

        Public Property FirstName As String
            Get
                Return GetProperty(Function() Me.FirstName)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() FirstName, value, New System.Action(AddressOf UpdateFullName))
            End Set
        End Property

        Public Property LastName As String
            Get
                Return GetProperty(Function() Me.LastName)
            End Get

            Set(ByVal value As String)
                SetProperty(Function() LastName, value, New System.Action(AddressOf UpdateFullName))
            End Set
        End Property

        Public ReadOnly Property FullName As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property

        Private Sub UpdateFullName()
            RaisePropertyChanged(Function() FullName)
        End Sub

        Protected Overrides Sub OnInitializeInDesignMode()
            MyBase.OnInitializeInDesignMode()
            FirstName = "FirstName in DesignMode"
            LastName = "LastName in DesignMode"
        End Sub

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            FirstName = "FirstName"
            LastName = "LastName"
        End Sub

        Private ReadOnly Property MessageBoxService As IMessageBoxService
            Get
                Return GetService(Of IMessageBoxService)()
            End Get
        End Property

        Public ReadOnly Property ShowMessageCommand As DelegateCommand(Of String)

        Private Sub ShowMessage(ByVal message As String)
            MessageBoxService.Show(message)
        End Sub

        Private Function CanShowMessage(ByVal message As String) As Boolean
            Return Not String.IsNullOrEmpty(message)
        End Function

        Public Sub New()
            ShowMessageCommand = New DelegateCommand(Of String)(AddressOf ShowMessage, AddressOf CanShowMessage)
        End Sub

        <Command>
        Public Sub Register()
            MessageBoxService.Show(FullName)
        End Sub

        Public Function CanRegister() As Boolean
            Return Not(String.IsNullOrEmpty(FirstName) OrElse String.IsNullOrEmpty(LastName))
        End Function
    End Class
End Namespace
