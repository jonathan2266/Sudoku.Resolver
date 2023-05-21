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
        stage ('Clean workspace') {
			steps {
				cleanWs()
			}
		}
    }
}