pipeline {
    agent any

    stages {
		tage('Setting the variables values') {
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