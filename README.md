# GXserver-Slack-Commit-Hook
GXServer-Slack integration using [Event Dispatcher extension](https://wiki.genexus.com/commwiki/servlet/wiki?46160).

## This repository contains:

An example of the implementation of the intermediate software for the communication between GeneXus Server and Slack. 

The Event Dispatcher extension (installed on GeneXus Server) is subscribed to the commits of the KB an calls this intermediate software.

## Repository structure:

- GeneXus.Server.ExternalTool.Slack

## Requirements

 - .NET Framework 4.7.1 or upper
 -  GeneXus Platform SDK
 
  At runtime, the Event Dispatcher extension has to be installed at GeneXus Server.

## How build the solution

 - Defined the $(GX_SDK_DIR) environment variable to the Platform SDK installation.
 - Check the .csproj file where it detemines where the compiled dll will be copied (define the $(GX_SERVER_DIR) environment variable to the <GeneXus Server installation>\VDIR).
   In order to debug or run this solution the compiled dll has to be under the $(GX_SERVER_DIR)\BinGenexus folder.

# License

   Copyright 2020 GeneXus

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
