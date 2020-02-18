# Akka.NET Demo Project

**Technologies Used:** C#, .NET Core, [Akka.NET](https://getakka.net/) (Actor Model Implementation), Docker, Docker-Compose

## Overview

[Akka.NET](https://getakka.net/) demo project consists of multiple solutions.

## MoviePlaybackSystem Solution

**Added feature list:**

1. Git Branch: "feature/001-basic-akka.net-system"
    1. Added basic [Akka.NET](https://getakka.net/articles/intro/what-is-akka.html) application with lifecycle hooks
2. Git Branch: "feature/002-docker-support"
    1. Added Docker support
3. Git Branch: "mps/feature/003-actorlifecycle-behavior-state"
    1. Added actor behavior and state, refactored code
4. Git Branch: "mps/feature/04-hierarchies-and-isolating-faults"
    1. Added actor hierarchies and fault isolation
    2. Created ActorSystem abstractions
5. Git Branch: "mps/feature/005-fixhierarchy-and-refactoring"
    1. Fixed Actor hierarchy
    2. Added Petabridge.Cmd
    3. Added ActorMetaData and refactored code
    4. Added user interactive session using CommandLine parser
6. Git Branch: "mps/feature/006-remoting-actors"
    1. Added Remote Actor support
    2. Added "MoviePlaybackSystem.Remote.PlaybackStatisticsActor" project to host Remote "PlaybackStatisticsActor" actor
    3. More refactoring...