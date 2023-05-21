pipeline {
    agent any

    stages {
		stage('Setting the variables values') {
			steps {
				sh '''#!/bin/bash
				ls
                 echo "hello world" 
				'''
			}
		}
        stage ('Restore Packages') {
			steps {
				sh '''#!/bin/bash
				dotnet restore
				'''
			}
		}
    }
}