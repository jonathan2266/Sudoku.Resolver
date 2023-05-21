pipeline {
    agent any

    stages {
        stage ('Restore Packages') {
			steps {
				sh '''#!/bin/bash
				dotnet restore
				'''
			}
		}
		stage('Test') {
			steps {
				sh 'dotnet test'
			}
		}
		stage('Pack Nugets') {
			steps {
				sh 'dotnet pack ./Sudoku/Sudoku.csproj -c Release'
				sh '''#!/bin/bash
				OUTPUT=$(find ./ -name Sudoku.*.nupkg)
				dotnet nuget push ./${OUTPUT} -s http://192.168.1.17:3000//api/packages/Home/nuget/index.json --skip-duplicate
				'''
			}
			steps {
				sh 'dotnet pack ./Sudoku.Parser/Sudoku.Parser.csproj -c Release'
				sh '''#!/bin/bash
				OUTPUT=$(find ./ -name Sudoku.Parser.*.nupkg)
				dotnet nuget push ./${OUTPUT} -s http://192.168.1.17:3000//api/packages/Home/nuget/index.json --skip-duplicate
				'''
			}
			steps {
				sh 'dotnet pack ./Sudoku.Parser.Web/Sudoku.Parser.Web.csproj -c Release'
				sh '''#!/bin/bash
				OUTPUT=$(find ./ -name Sudoku.Parser.Web.*.nupkg)
				dotnet nuget push ./${OUTPUT} -s http://192.168.1.17:3000//api/packages/Home/nuget/index.json --skip-duplicate
				'''
			}
			steps {
				sh 'dotnet pack ./Sudoku.Parser.File/Sudoku.Parser.File.csproj -c Release'
				sh '''#!/bin/bash
				OUTPUT=$(find ./ -name Sudoku.Parser.File.*.nupkg)
				dotnet nuget push ./${OUTPUT} -s http://192.168.1.17:3000//api/packages/Home/nuget/index.json --skip-duplicate
				'''
			}	
		}
    }
}